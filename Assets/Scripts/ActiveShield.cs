using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShield : MonoBehaviour
{
    public GameObject shield;
    public static int shieldUpgrade = 0;
    public static bool shieldToggle = false;
    public static bool shieldActive = false;
    public static bool shieldBroken = false;

    // Start is called before the first frame update
    void Start()
    {
        shieldToggle = true;
        shieldBroken = false;
        shieldActive = false;
        shield.SetActive((shieldUpgrade==1) ? true : false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldToggle && shieldUpgrade==1) {
            shield.SetActive(true);
            shieldToggle = false;
            shieldBroken = false;
            shieldActive = true;
        }

        if (shieldBroken) {
            shield.SetActive(false);
            shieldBroken = false;
            shieldActive = false;
        }
    }
}
