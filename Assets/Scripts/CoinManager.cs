using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static int coinFrequencyUpgrade = 0;
    public static int coinFrequencyCounter = 0;
    public static int coinTypeUpgrade = 0;
    public static int coinValue = 553330; // Change to 553330 for demo, but change back to 0 for release
    public static bool coinUpdated = false;
    public static bool coinToBeRandomized = false;
    public Transform coin;
    public TextMeshProUGUI coinGUI;
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        coinGUI.text = "Coins: " + coinValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (coinUpdated) {
            coinGUI.text = "Coins: " + coinValue;
            coinUpdated = false;
        }

        if (coinToBeRandomized) {
            RandomizeCoin();
            coinToBeRandomized = false;
        }
    }

    void RandomizeCoin() {
        int coinType = CoinCollect.coinTypeMatrix[coinTypeUpgrade, Random.Range(0, 10)];

        if (coinType == 0) {
            coin.gameObject.tag = "RubyCoin";
            ChangeMaterials(coin, "Ruby");
        } else if (coinType == 1) {
            coin.gameObject.tag = "SilverCoin";
            ChangeMaterials(coin, "Silver");
        } else if (coinType == 2) {
            coin.gameObject.tag = "GoldCoin";
            ChangeMaterials(coin, "Gold");
        } else if (coinType == 3) {
            coin.gameObject.tag = "DiamondCoin";
            ChangeMaterials(coin, "Diamond");
        }
    }

    void ChangeMaterials(Transform coin, string materialName)
    {
        // Target material
        Material newMaterial = FindMaterialByName(materialName);
        if (newMaterial == null) return;

        // Iterate through all child GameObjects of coin
        foreach (Transform c in coin)
        {
            // If child has render, update material
            Renderer renderer = c.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Change the material of the Renderer component
                Material[] rendMat = renderer.sharedMaterials;
                for (int i = 0; i < rendMat.Length; i++)
                {
                    rendMat[i] = newMaterial;
                }
                renderer.materials = rendMat;
            }
        }
    }

    Material FindMaterialByName(string materialName)
    {
        foreach (Material mat in materials)
        {
            if (mat.name == materialName)
            {
                return mat;
            }
        }
        return null;
    }
}
