using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int enemySpawnFrequencyCounter;
    public static int enemySpawnMin = 1; // default: 1
    public static int enemySpawnMax = 7; // default: 7

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnFrequencyCounter = Random.Range(enemySpawnMin, enemySpawnMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
