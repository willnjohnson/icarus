using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureEight : MonoBehaviour
{

    public float speed = 1.0f;
    public float amplitude = 1.0f;
    public float delay = 0.0f;

    private float T = 0.0f;
    private Vector3 pos;


    void Start() {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime * speed;

        // Change Y and Z to create Figure-Eight motion
        float z = Mathf.Sin(delay + T) * amplitude;
        float y = Mathf.Sin(delay + 2 * T) * amplitude / 2;

        // Set the object's position
        transform.position = pos + new Vector3(0, y, z);
    }
}
