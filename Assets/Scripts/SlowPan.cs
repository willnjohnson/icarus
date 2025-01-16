using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPan : MonoBehaviour
{
    public float minZ = -12.5f;
    public float maxZ = -10f;
    public float speed = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.PingPong(Time.time * speed, maxZ - minZ) + minZ;
    
        Vector3 newPosition = transform.position;
        newPosition.z = z;
        transform.position = newPosition;
    }
}
