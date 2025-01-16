using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafFall : MonoBehaviour
{
    public float oscillationRange = 2.0f;
    public float oscillationSpeed = 0.5f;
    public float fallSpeed = -1.00f;
    public float alpha = 0.0f;
    public float fadeDuration = 1.0f;

    private Material material;
    private Vector3 startPosition;
    private bool isFading;

    // Start is called before the first frame update
    void Start()
    {
        // Start with low alpha
        material = GetComponent<Renderer>().material;
        Color materialColor = material.color;
        materialColor.a = 0.0f;
        material.color = materialColor;

        startPosition = transform.position;
        isFading = true;
    }

    // Update is called once per frame
    void Update()
    {
        float oscillationValue = Mathf.Lerp(-oscillationRange, oscillationRange, Mathf.PingPong(Time.time * oscillationSpeed, 1f));
        transform.Translate(oscillationValue * Time.deltaTime, fallSpeed * Time.deltaTime, 0);

        // Smoothly transition from low alpha to higher alpha to give illusion of leaf coming out of tree
        if (isFading) {
            // Fade alpha value
            alpha += Time.deltaTime / fadeDuration;

            if (alpha > 1.0f) {
                isFading = false;
            } else {
                // Make color alpha more prominent
                Color materialColor = material.color;
                materialColor.a = alpha;
                material.color = materialColor;
            }
        }

        // Reset falling leaf
        if (transform.position.y < -29.0f) {
            transform.position = startPosition;
            isFading = true;
            Color materialColor = material.color;
            alpha = 0.0f;
            materialColor.a = alpha;
            material.color = materialColor;
        }
    }
}
