using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVerticalWraparound : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
    public bool isLeft;
    public float offset;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = isLeft ? -Mathf.Abs(speed) : Mathf.Abs(speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        // Wraparound, if position is offscreen (based on x that we defined as being offscreen with extra consideration of z)
        if (isLeft && transform.position.x < xMin)
        {
            transform.position = new Vector3(xMax + offset, transform.position.y, zMax + offset);
        }
        else if (!isLeft && transform.position.x > xMax)
        {
            transform.position = new Vector3(xMin - offset, transform.position.y, zMin - offset);
        }
    }
}
