using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGlow : MonoBehaviour
{
    public Light sunlight;
    public float minIntensity = 1.0f;
    public float maxIntensity = 2.5f;
    public float minScale = 1.0f;
    public float maxScale = 1.1f;
    public float intensitySpeed = 2.0f;

    private float timeIntensity = 0.0f;
    private Vector3 minSize;
    private Vector3 maxSize;

    // Sun material and gold (base) color
    private Material material;
    private Color baseColor;
    private Color baseEmissionColor;

    // Sun into cream (target) color
    private Color targetColor = new Color(255f/255f, 240f/255f, 194f/255f);
    private Color targetEmissionColor = new Color(255f/255f, 221f/255f, 167/255f);

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        baseColor = material.color;
        baseEmissionColor = material.GetColor("_EmissionColor");

        minSize = transform.localScale * minScale;
        maxSize = transform.localScale * maxScale;
    }

    // Update is called once per frame
    void Update()
    {
        timeIntensity += Time.deltaTime;

        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(timeIntensity * intensitySpeed));
        sunlight.intensity = intensity;

        Vector3 sunSize = Vector3.Lerp(minSize, maxSize, Mathf.Sin(timeIntensity * intensitySpeed));
        transform.localScale = sunSize;

        Color lerpedColor = Color.Lerp(baseColor, targetColor, Mathf.Sin(timeIntensity * intensitySpeed));
        Color lerpedEmissionColor = Color.Lerp(baseEmissionColor, targetEmissionColor, Mathf.Sin(timeIntensity * intensitySpeed));
        material.color = lerpedColor;
        material.SetColor("_EmissionColor", lerpedEmissionColor);
    }
}
