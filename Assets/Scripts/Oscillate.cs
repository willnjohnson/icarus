using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Redbull gives you wings?
public class Oscillate : MonoBehaviour {
    public GameObject mainObject;           // Set a reference to the main object (so object stays relatively close to main object)
    public float amplitudeTranslate = 1.5f;   // How far to translate object
    public float amplitudeRotate = 25.0f;   // How far to rotate object
    public float frequency = 2.5f;          // Frequency of oscillation in cycles per second

    // Initial values for position and rotation
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the initial position of the current object relative to the main object
        initialPosition = transform.position - mainObject.transform.position;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the offset based on oscillation
        float offsetY = amplitudeTranslate * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);
        float angleOffset = amplitudeRotate * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);

        // Calculate the new position of the current object relative to the main object
        Vector3 newPosition = mainObject.transform.position + initialPosition + new Vector3(0.0f, offsetY, 0.0f);
        Quaternion newRotation = initialRotation * Quaternion.Euler(angleOffset, 0.0f, 0.0f);

        // Update the Upper object's position
        transform.position = newPosition;
        transform.localRotation = newRotation;
    }
}