using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDisplacement : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = targetTransform.position + offset;
        transform.position = newPosition;
    }
}
