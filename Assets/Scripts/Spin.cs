using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Let user define axis of rotation and speed
    public enum Axis {X, Y, Z};
    public Axis axis;
    public float rotationSpeed = 100.00f;
    private Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        switch (axis) {
            case Axis.X:
                vec = Vector3.right;
                break;
            case Axis.Y:
                vec = Vector3.up;
                break;
            default:
                vec = Vector3.forward;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vec, rotationSpeed * Time.deltaTime);
    }
}
