using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushAnimation : MonoBehaviour
{
    public float xOffset;
    public float yOffset;
    public float zOffset;

    public float duration = 1.0f;

    private float startTime;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveShield.shieldBroken) {
            startTime = Time.time;
            isMoving = true;
        }

        // Shield crush animation
        if (isMoving) {
            GetComponent<Renderer>().enabled = true;

            float elapsedTime = Time.time - startTime;

            // Time t represents how animation progresses
            float t = Mathf.Clamp01(elapsedTime / duration);

            // New position based on offsets
            Vector3 offset = new Vector3(xOffset * t, yOffset * t, zOffset * t);

            // Translate object by offset
            transform.Translate(offset, Space.Self);

            // Interpolate object's alpha based on time t
            Color color = GetComponent<Renderer>().material.color;
            color.a = Mathf.Lerp(0.6f, 0.0f, t);
            GetComponent<Renderer>().material.color = color;

            // If elapsedTime >= duration, done moving object
            if (elapsedTime >= duration)
            {
                color.a = 0.0f;
                GetComponent<Renderer>().material.color = color;
                isMoving = false;
                GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
