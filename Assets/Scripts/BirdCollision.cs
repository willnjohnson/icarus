using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCollision : MonoBehaviour
{
    private GameObject bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (ActiveShield.shieldActive) {
                // Enemy destroyed and shield broken
                bird.SetActive(false);
                EnemyManager.enemySpawnFrequencyCounter = Random.Range(EnemyManager.enemySpawnMin, EnemyManager.enemySpawnMax);
                ActiveShield.shieldBroken = true;
            } else {
                // Player defeated
                PlayerMovement.isDefeated = true;
            }
        }
    }
}
