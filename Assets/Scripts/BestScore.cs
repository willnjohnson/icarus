using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    private float scoreOffset = -4.989079f;
    public TextMeshProUGUI scoreBestGUI;

    // Start is called before the first frame update
    void Start()
    {
        if ((-(ScoreManager.scoreBest) + scoreOffset) > 0)
            scoreBestGUI.text = "Best Score: " + (-(ScoreManager.scoreBest) + scoreOffset);
        else
            scoreBestGUI.text = "Best Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
