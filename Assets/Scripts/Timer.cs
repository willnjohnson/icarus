using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float currentTime;
    public TextMeshProUGUI timerGUI;            // Timer
    public static int timeLengthUpgrade = 0;    // Initial current time is determined by upgrade
    public bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start time is determined by upgrade 

        currentTime = GetTime();
        SetTime(currentTime);
    }

    void SetTime(float currentTime) {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int microseconds = Mathf.FloorToInt((currentTime - Mathf.Floor(currentTime)) * 1000); // Get microseconds (rounded to 3 digits)

        timerGUI.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, microseconds);
    }

    // Update is called once per frame
    void Update()
    {
        // Update timer
        if (currentTime > 0.0f) {
            currentTime -= Time.deltaTime;

            SetTime(currentTime);
        }

        // Reset timer
        if (reset) 
        {
            currentTime = GetTime();
            reset = false;
        }

        // Check if clock object was collected
        if (ClockManager.clockUpdated) {
            currentTime += ClockManager.timeExtendMatrix[ClockManager.timeExtendUpgrade];
            ClockManager.clockUpdated = false;
        }
    }

    float GetTime() {
        if (timeLengthUpgrade == 4)
            return 420.0f;

        if (timeLengthUpgrade == 3)
            return 300.0f;

        if (timeLengthUpgrade == 2)
            return 180.0f;

        if (timeLengthUpgrade == 1)
            return 150.0f;

        return 120.0f; 
    }
}
