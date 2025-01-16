using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public Transform clock;

    public static int timeExtendUpgrade = 0;
    public static float[] timeExtendMatrix = new float[5];
    public static bool clockUpdated;
    public static int clockSpawnFrequencyCounter;
    public static int clockSpawnMin = 10; // default: 10
    public static int clockSpawnMax = 30; // default: 30

    // Start is called before the first frame update
    void Start() {
        // Clock Length for each upgrade value index
        timeExtendMatrix[0] = 1f;
        timeExtendMatrix[1] = 3f;
        timeExtendMatrix[2] = 5f;
        timeExtendMatrix[3] = 7f;
        timeExtendMatrix[4] = 10f;

        clockUpdated = false;

        clockSpawnFrequencyCounter = Random.Range(clockSpawnMin, clockSpawnMax);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clock")) {
            // Set clock updated to true, so Timer recognizes that it needs to update the timer
            clockUpdated = true;

            // Un-activate clock and reset spawn counter
            clock.gameObject.SetActive(false);
            clockSpawnFrequencyCounter = Random.Range(clockSpawnMin, clockSpawnMax);
        }
    }
}
