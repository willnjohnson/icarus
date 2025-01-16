using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI[] UpgradeValueGUI = new TextMeshProUGUI[5];
    public TextMeshProUGUI[] UpgradeTextGUI = new TextMeshProUGUI[5];
    public TextMeshProUGUI[] UpgradeButtonGUI = new TextMeshProUGUI[5];
    public TextMeshProUGUI coinGUI;
    public int[] UV = new int[5];
    public static bool updateStatus;
    public static int upgradeToBePurchased; // -1: None, 0: Time Rewind, 1: Strting Clock, 2: Coin Spawn Rate, 3: Coin Rarity, 4: Shield

    // Start is called before the first frame update
    void Start()
    {
        upgradeToBePurchased = -1;
        updateStatus = false;

        UpgradeValueGUI[0].text = "20";
        UpgradeValueGUI[1].text = "20";
        UpgradeValueGUI[2].text = "20";
        UpgradeValueGUI[3].text = "20";
        UpgradeValueGUI[4].text = "2500";

        UpgradeTextGUI[0].text = "Rewind Time [0/4]";
        UpgradeTextGUI[1].text = "Longer Starting Clock [0/4]";
        UpgradeTextGUI[2].text = "Increase Coin Spawn Rate [0/4]";
        UpgradeTextGUI[3].text = "Increase Coin Rarity [0/4]";
        UpgradeTextGUI[4].text = "Shield [0/1]";
    }

    void SetStatus() {
        UV[0] = 20 * (int)Mathf.Pow(4, ClockManager.timeExtendUpgrade);
        UV[1] = 20 * (int)Mathf.Pow(4, Timer.timeLengthUpgrade);
        UV[2] = 20 * (int)Mathf.Pow(4, CoinManager.coinFrequencyUpgrade);
        UV[3] = 20 * (int)Mathf.Pow(4, CoinManager.coinTypeUpgrade);
        UV[4] = 2500;

        UpgradeValueGUI[0].text = (ClockManager.timeExtendUpgrade>=4) ? "MAX" : ("" + UV[0]);
        UpgradeValueGUI[1].text = (Timer.timeLengthUpgrade>=4) ? "MAX" : ("" + UV[1]);
        UpgradeValueGUI[2].text = (CoinManager.coinFrequencyUpgrade>=4) ? "MAX" : ("" + UV[2]);
        UpgradeValueGUI[3].text = (CoinManager.coinTypeUpgrade>=4) ? "MAX" : ("" + UV[3]);
        UpgradeValueGUI[4].text = (ActiveShield.shieldUpgrade>=1) ? "MAX" : ("" + UV[4]);

        UpgradeTextGUI[0].text = "Rewind Time [" + ClockManager.timeExtendUpgrade + "/4]";
        UpgradeTextGUI[1].text = "Longer Starting Clock [" + Timer.timeLengthUpgrade + "/4]";
        UpgradeTextGUI[2].text = "Increase Coin Spawn Rate [" + CoinManager.coinFrequencyUpgrade + "/4]";
        UpgradeTextGUI[3].text = "Increase Coin Rarity [" + CoinManager.coinTypeUpgrade + "/4]";
        UpgradeTextGUI[4].text = "Shield [" + ActiveShield.shieldUpgrade + "/1]";

        SetColor(0);
        SetColor(1);
        SetColor(2);
        SetColor(3);
        SetColor(4);

        if (upgradeToBePurchased >= 0 && upgradeToBePurchased <= 4) {
            int deductible = UV[upgradeToBePurchased];

            if (CoinManager.coinValue >= deductible) {
                if (upgradeToBePurchased == 0 && ClockManager.timeExtendUpgrade < 4) {
                    UpdatePurchase(deductible);
                    ClockManager.timeExtendUpgrade++;
                } else if (upgradeToBePurchased == 1 && Timer.timeLengthUpgrade < 4) {
                    UpdatePurchase(deductible);
                    Timer.timeLengthUpgrade++;
                } else if (upgradeToBePurchased == 2 && CoinManager.coinFrequencyUpgrade < 4) {
                    UpdatePurchase(deductible);
                    CoinManager.coinFrequencyUpgrade++;
                } else if (upgradeToBePurchased == 3 && CoinManager.coinTypeUpgrade < 4) {
                    UpdatePurchase(deductible);
                    CoinManager.coinTypeUpgrade++;
                } else if (upgradeToBePurchased == 4 && ActiveShield.shieldUpgrade < 1) {
                    UpdatePurchase(deductible);
                    ActiveShield.shieldUpgrade++;
                }
            }
            upgradeToBePurchased = -1;
        }
    }

    void UpdatePurchase(int deductible) {
        CoinManager.coinValue = CoinManager.coinValue - deductible;
        CoinManager.coinUpdated = true;
        coinGUI.text = "Coins: " + CoinManager.coinValue;
    }

    void SetColor(int i) {
        if (CoinManager.coinValue < UV[i] || UpgradeValueGUI[i].text == "MAX") {
            UpgradeValueGUI[i].color = Color.red;
            UpgradeTextGUI[i].color = Color.red;
            UpgradeButtonGUI[i].color = Color.red;
        } else {
            UpgradeValueGUI[i].color = Color.white;
            UpgradeTextGUI[i].color = Color.white;
            UpgradeButtonGUI[i].color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (updateStatus) {
            SetStatus();
        }
    }
}
