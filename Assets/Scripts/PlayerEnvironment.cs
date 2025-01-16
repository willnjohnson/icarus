using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

// Handles events relating to how player moves, falls out of the map, and collision
public class PlayerMovement : MonoBehaviour
{
    private Vector3 respawnPosition;
    private Vector3[] platformRespawnPosition = new Vector3[3];
    private Vector3 platformNextPosition;
    private float inHorizontal;
    private float inVertical;
    private Rigidbody rb;

    private bool isGrounded = false;             // isGrounded checks if collision occurs
    private bool isShiftHeld = false;       // isShiftHeld creates greater downward velocity of Player
    private bool isLocked = false;          // isLocked helps with "unshifting" even if shift is still held
    private float airTime = 0f;             // Airtime allows reset, if Player spends too long
    private int platformIndex = 0;          // Index determines platform generation
    private int nPlatforms = 3;             // Total number of platforms
    private GameObject[] upgradeObjects;
    private GameObject[] countdownObjects;
    private TrailRenderer[] trailRenderers;
    private TrailRenderer[] startTrailRenderers;

    public static bool isCloseMenu;
    public static bool isDefeated;
    public static float enemyFade;
    public Timer timer;
    public ScoreManager score;
    public Transform pivot;
    public Transform[] platform = new Transform[3]; // Set within inspector to each "Platform#" where # = 1, 2, 3
    public Transform coin;                  // Let platform spawn dictate coin spawn
    public Transform clock;                 //   as well as how clock object spawns
    public Transform enemy;                 //   and the enemy spawn
    public float moveForce = 15f;           // WASD directions
    public float jumpForce = 1000f;         // Normal jump force
    public float jumpForceBonus = 550f;     // Increased gravity gives extra bounce
    public float gravity = 0f;              // Default gravity
    public float increasedGravity = -80f;   // Gravity when Shift is held
    public bool reset = false;
    public bool pauseToggle = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialized objects and values
        rb = GetComponent<Rigidbody>();

        respawnPosition = transform.position;
        
        platformRespawnPosition[0] = platform[0].position;
        platformRespawnPosition[1] = platform[1].position;
        platformRespawnPosition[2] = platform[2].position;

        // Keep copy of starting trail
        trailRenderers = FindObjectsOfType<TrailRenderer>();
        startTrailRenderers = new TrailRenderer[trailRenderers.Length];
        for (int i = 0; i < trailRenderers.Length; i++)
        {
            startTrailRenderers[i] = trailRenderers[i];
        }
        ClearAndResetTrails();

        isDefeated = false;
        isCloseMenu = false;
        score.reset = true;
        CoinManager.coinToBeRandomized = true;
        coin.gameObject.SetActive(false);
        clock.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        CoinManager.coinFrequencyCounter = Random.Range(CoinCollect.coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,0], CoinCollect.coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,1] + 1);

        upgradeObjects = GameObject.FindGameObjectsWithTag("Upgrade");
        countdownObjects = GameObject.FindGameObjectsWithTag("Countdown");
        ToggleManager.ComponentsSetToggle(upgradeObjects, false);
        ToggleManager.ComponentsSetToggle(countdownObjects, true);

        pauseToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pausing
        if (Input.GetKeyUp(KeyCode.U) || isCloseMenu) {
            if (pauseToggle == false) {
                pauseToggle = true;

                // Allow for interaction with buttons in upgrade menu when pause game
                ButtonInteraction.menuInteractable = true;

                // Buffer the reset (in case user decides to open menu, we want player to appear back at spawn)
                platform[0].position = platformRespawnPosition[0];
                platform[1].position = platformRespawnPosition[1];
                platform[2].position = platformRespawnPosition[2];
                coin.position = respawnPosition;
                coin.gameObject.SetActive(false);
                clock.position = respawnPosition;
                clock.gameObject.SetActive(false);
                enemy.position = respawnPosition;
                enemyFade = 1.0f;
                enemy.gameObject.SetActive(false);
                score.reset = true;
                CoinManager.coinToBeRandomized = true;
                ActiveShield.shieldToggle = true;
                ClearAndResetTrails();

                // Update upgrade status
                UpgradeManager.updateStatus = true;

                ToggleManager.ComponentsSetToggle(upgradeObjects, true);
                ToggleManager.ComponentsSetToggle(countdownObjects, false);
            } else if (pauseToggle == true) {
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                // Disallow for interaction with buttons in upgrade menu when resume game
                ButtonInteraction.menuInteractable = false;

                // Commit to the true reset
                reset = true;
                timer.reset = true;
                pauseToggle = false;
                isCloseMenu = false;

                ToggleManager.ComponentsSetToggle(upgradeObjects, false);
                ToggleManager.ComponentsSetToggle(countdownObjects, true);
            }
        }

        if (pauseToggle == true) {
            transform.position = respawnPosition;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            ActiveShield.shieldToggle = true;
            ClearAndResetTrails();
        } else {
            // Input
            inHorizontal = Input.GetAxis("Horizontal");
            inVertical = Input.GetAxis("Vertical");
            isShiftHeld = Input.GetKey(KeyCode.LeftShift);

            // Intensify gravity upon left shift hold, otherwise normal gravity
            if (!isLocked && isShiftHeld) {
                rb.AddForce(new Vector3(0, increasedGravity, 0), ForceMode.Acceleration);
            } else {
                rb.AddForce(new Vector3(0, gravity, 0), ForceMode.Acceleration);
            }

            // Release lock, when player let's go of shift
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                isLocked = false;
            }

            // Player moves
            transform.Translate(new Vector3(0, 0, moveForce * inVertical * Time.deltaTime));
            transform.Translate(new Vector3(moveForce * inHorizontal * Time.deltaTime, 0, 0));

            // Check if Player is far away from farthest-back Platform to determine if it should be respawned
            if ((transform.position.z - platform[platformIndex % 3].position.z) < -15) 
            {
                MovePlatform();
            }

            // Also handle despawn of other objects
            if (coin.gameObject.activeSelf && ((transform.position.z - coin.position.z) < -15))
            {
                // Whenever we deactivate (coin) object, we want to reset counter
                coin.gameObject.SetActive(false);
                CoinManager.coinFrequencyCounter = Random.Range(CoinCollect.coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,0], CoinCollect.coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,1] + 1);
            }

            if (clock.gameObject.activeSelf && ((transform.position.z - clock.position.z) < -15))
            {
                // Whenever we deactivate (clock) object, we want to reset counter
                clock.gameObject.SetActive(false);
                ClockManager.clockSpawnFrequencyCounter = Random.Range(ClockManager.clockSpawnMin, ClockManager.clockSpawnMax);
            }

            if (enemy.gameObject.activeSelf && enemyFade == 1.0f) {
                EnemyManager.enemySpawnFrequencyCounter = Random.Range(EnemyManager.enemySpawnMin, EnemyManager.enemySpawnMax);
                enemy.gameObject.SetActive(false);
            }

            // Check if isGrounded is set to determine whether to jump
            if (isGrounded)
            {
                rb.AddForce(new Vector3(0, isLocked ? (jumpForce+jumpForceBonus) : jumpForce, 0), ForceMode.Impulse);
                isGrounded = false;
            } else {
                airTime += Time.deltaTime;

                // Too long in the air OR clock has reached zero, need to reset
                if (airTime > 5f || timer.currentTime <= 0.0f) {
                    reset = true;
                    score.reset = true;
                    timer.reset = true;
                }
            }

            if (Input.GetKey(KeyCode.R) || isDefeated == true) {
                isDefeated = false;
                reset = true;
                score.reset = true;
                timer.reset = true;
                ActiveShield.shieldToggle = true;
                ClearAndResetTrails();
            }

            // Check if reset
            if (reset)
            {
                // Reset platforms and index
                platform[0].position = platformRespawnPosition[0];
                platform[1].position = platformRespawnPosition[1];
                platform[2].position = platformRespawnPosition[2];
                platformIndex = 0;

                // Respawn coin
                coin.position = respawnPosition;
                coin.gameObject.SetActive(false);

                // Respawn clock
                clock.position = respawnPosition;
                clock.gameObject.SetActive(false);

                // Respawn enemy
                enemy.position = respawnPosition;
                enemyFade = 1.0f;
                enemy.gameObject.SetActive(false);

                // Reset player
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                transform.position = respawnPosition;
                rb.velocity = Vector3.zero;             // Prevents sudden fall on respawn

                // Reset airtime
                airTime = 0f;
                reset = false;

                // Reactivate shield if upgrade already acquired and reset trails
                ActiveShield.shieldToggle = true;
                ClearAndResetTrails();
            }
        }
    }

    // Check collision on entry
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (isShiftHeld) {
                isLocked = true;
            }
            // Reset airTime
            airTime = 0f;

            // Respawn farthest platform
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        // Move platform that was collided to a new position (so that the game looks like it's endless)
        platformNextPosition = platform[(platformIndex + (nPlatforms-1)) % nPlatforms].position;
        platformNextPosition.x += Random.Range(Random.value < 0.5f ? -15.0f : 10.0f, Random.value < 0.5f ? -10.0f : 15.0f);
        platformNextPosition.y += 5.25f;
        platformNextPosition.z -= 10.0f;
        platform[platformIndex % 3].position = platformNextPosition;
        platformIndex += 1;

        // Relocate coin
        if (CoinManager.coinFrequencyCounter <= 0-nPlatforms)
        {
            // Spawn a randomized coin on farthest platform and reset counter
            Vector3 coinNextPosition = new Vector3(platformNextPosition.x, platformNextPosition.y, platformNextPosition.z);

            CoinManager.coinToBeRandomized = true;
            coin.position = coinNextPosition;
            coin.gameObject.SetActive(true);
            CoinManager.coinFrequencyCounter = Random.Range(CoinCollect.coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,0], CoinCollect.coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,1] + 1);
        } else {
            // Decrement counter
            CoinManager.coinFrequencyCounter--;
        }

        // Relocate clock object
        if (ClockManager.clockSpawnFrequencyCounter <= 0-nPlatforms)
        {
            // Spawn clock on farthest platform (but random X and Z, so that it's not in direct center to give player more challenge to collect item)
            float clockSpawnRandX = Random.Range(platformNextPosition.x - 2, platformNextPosition.x + 2);
            float clockSpawnRandZ = Random.Range(platformNextPosition.z - 4, platformNextPosition.z + 4);

            Vector3 clockNextPosition = new Vector3(clockSpawnRandX, platformNextPosition.y + 4, clockSpawnRandZ);

            clock.position = clockNextPosition;
            clock.gameObject.SetActive(true);
            ClockManager.clockSpawnFrequencyCounter = Random.Range(ClockManager.clockSpawnMin, ClockManager.clockSpawnMax);
        } else {
            // Decrement spawn counter
            ClockManager.clockSpawnFrequencyCounter--;
        }

        // Relocate enemy
        if (EnemyManager.enemySpawnFrequencyCounter <= 0-nPlatforms)
        {
            // Since enemy flys from right to left, it's better for enemy to spawn from right
            // Bit of randomness sprinkled in for fun
            float enemySpawnRandX = Random.Range(platformNextPosition.x + 9, platformNextPosition.x + 15);
            float enemySpawnRandY = Random.Range(platformNextPosition.y + 5.5f, platformNextPosition.y + 8.0f);
            float enemySpawnRandZ = Random.Range(platformNextPosition.z - 4, platformNextPosition.z + 4);

            Vector3 enemyNextPosition = new Vector3(enemySpawnRandX, enemySpawnRandY, enemySpawnRandZ);

            enemy.position = enemyNextPosition;
            enemyFade = 0.0f;
            enemy.gameObject.SetActive(true);
            EnemyManager.enemySpawnFrequencyCounter = Random.Range(EnemyManager.enemySpawnMin, EnemyManager.enemySpawnMax);
        } else {
            // Decrement spawn counter
            EnemyManager.enemySpawnFrequencyCounter--;
        }
    }

    private void ClearAndResetTrails() {
        for (int i = 0; i < trailRenderers.Length; i++)
        {
            // Clear and reset values to start values
            trailRenderers[i].Clear();
            trailRenderers[i].time = startTrailRenderers[i].time;
            trailRenderers[i].startWidth = startTrailRenderers[i].startWidth;
            trailRenderers[i].endWidth = startTrailRenderers[i].endWidth;
        }
    }
}
