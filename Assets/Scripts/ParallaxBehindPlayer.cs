using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehindPlayer : MonoBehaviour
{
    public Transform Player; // Player object to follow

    public float xOff = 0;
    public float yOff = 0;
    public float zOff = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Starting position
        Vector3 newPosition = Player.position;

        transform.position = new Vector3(newPosition.x + xOff, newPosition.y + yOff, newPosition.z + zOff);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Player.position;

        // Clouds should follow player 
        transform.position = new Vector3(transform.position.x, newPosition.y + yOff, newPosition.z + zOff);

    }
}
