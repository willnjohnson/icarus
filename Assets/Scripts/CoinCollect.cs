using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public Transform coin;
    public static int[,] coinFrequencyMatrix = new int[5, 2];
    public static int[,] coinTypeMatrix = new int[5, 10];

    void Start() {
        // x (index by coin frequnecy upgrade value, 0-4)
        // y (0:min spawn rate, 1:max spawn rate)
        coinFrequencyMatrix[0, 0] = 5;
        coinFrequencyMatrix[0, 1] = 10;

        coinFrequencyMatrix[1, 0] = 4;
        coinFrequencyMatrix[1, 1] = 8;

        coinFrequencyMatrix[2, 0] = 3;
        coinFrequencyMatrix[2, 1] = 7;

        coinFrequencyMatrix[3, 0] = 0;
        coinFrequencyMatrix[3, 1] = 5;

        coinFrequencyMatrix[4, 0] = 0;
        coinFrequencyMatrix[4, 1] = 1;

        // x (index by coin type upgrade value, 0-4)
        // y (type of coin, 0:Ruby, 1:Silver, 2:Gold, 3:Diamond)
        coinTypeMatrix[0, 0] = 0;
        coinTypeMatrix[0, 1] = 0;
        coinTypeMatrix[0, 2] = 0;
        coinTypeMatrix[0, 3] = 0;
        coinTypeMatrix[0, 4] = 0;
        coinTypeMatrix[0, 5] = 0;
        coinTypeMatrix[0, 6] = 0;
        coinTypeMatrix[0, 7] = 1;
        coinTypeMatrix[0, 8] = 1;
        coinTypeMatrix[0, 9] = 1;

        coinTypeMatrix[1, 0] = 0;
        coinTypeMatrix[1, 1] = 0;
        coinTypeMatrix[1, 2] = 0;
        coinTypeMatrix[1, 3] = 1;
        coinTypeMatrix[1, 4] = 1;
        coinTypeMatrix[1, 5] = 1;
        coinTypeMatrix[1, 6] = 1;
        coinTypeMatrix[1, 7] = 1;
        coinTypeMatrix[1, 8] = 2;
        coinTypeMatrix[1, 9] = 2;

        coinTypeMatrix[2, 0] = 0;
        coinTypeMatrix[2, 1] = 0;
        coinTypeMatrix[2, 2] = 0;
        coinTypeMatrix[2, 3] = 1;
        coinTypeMatrix[2, 4] = 1;
        coinTypeMatrix[2, 5] = 1;
        coinTypeMatrix[2, 6] = 2;
        coinTypeMatrix[2, 7] = 2;
        coinTypeMatrix[2, 8] = 2;
        coinTypeMatrix[2, 9] = 3;

        coinTypeMatrix[3, 0] = 0;
        coinTypeMatrix[3, 1] = 0;
        coinTypeMatrix[3, 2] = 1;
        coinTypeMatrix[3, 3] = 1;
        coinTypeMatrix[3, 4] = 2;
        coinTypeMatrix[3, 5] = 2;
        coinTypeMatrix[3, 6] = 2;
        coinTypeMatrix[3, 7] = 2;
        coinTypeMatrix[3, 8] = 3;
        coinTypeMatrix[3, 9] = 3;

        coinTypeMatrix[4, 0] = 0;
        coinTypeMatrix[4, 1] = 1;
        coinTypeMatrix[4, 2] = 1;
        coinTypeMatrix[4, 3] = 2;
        coinTypeMatrix[4, 4] = 2;
        coinTypeMatrix[4, 5] = 2;
        coinTypeMatrix[4, 6] = 2;
        coinTypeMatrix[4, 7] = 3;
        coinTypeMatrix[4, 8] = 3;
        coinTypeMatrix[4, 9] = 3;
    }

    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check tag to determine value of the coin
        if (other.CompareTag("RubyCoin")) {
            CoinManager.coinValue += 1;
            Reset();
        } else if (other.CompareTag("SilverCoin")) {
            CoinManager.coinValue += 5;
            Reset();
        } else if (other.CompareTag("GoldCoin")) {
            CoinManager.coinValue += 15;
            Reset();
        } else if (other.CompareTag("DiamondCoin")) {
            CoinManager.coinValue += 50;
            Reset();
        }
    }

    private void Reset() {
        CoinManager.coinUpdated = true;
        coin.gameObject.SetActive(false);
        CoinManager.coinFrequencyCounter = Random.Range(coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,0], coinFrequencyMatrix[CoinManager.coinFrequencyUpgrade,1] + 1);
    }
}
