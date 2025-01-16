using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float score;
    private float scoreOffset = -4.989079f;         // Normalize to 0

    public bool reset = false;
    public static float scoreBest = 0.0f;
    public PlayerMovement playerMovement;
    public TextMeshProUGUI scoreGUI;                // Set within inspector to the "Score" TextMeshProGUI
    public TextMeshProUGUI scoreBestGUI;            // Set within inspector to the "Score_Best" TextMeshProGUI

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        scoreBestGUI.text = "Best Score: " + (-scoreBest+scoreOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (reset) 
        {
            // Update best score, if score is improved
            if (score < scoreBest) {
                scoreBest = score;
                scoreBestGUI.text = "Best Score: " + (-scoreBest+scoreOffset);

                reset = false;
            }

            score = 0.0f;
        }

        // Update score (when player's Z position is "more negative" than score) - score is based on negative Z direction
        if (playerMovement.transform.position.z < score) 
        {
            score = playerMovement.transform.position.z;
            scoreGUI.text = "Score: " + (-score+scoreOffset);
        }
    }
}
