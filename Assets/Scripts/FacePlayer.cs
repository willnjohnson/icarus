using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera should always face the Player
public class FacePlayer : MonoBehaviour
{
    public Transform player;
    private float zOffset = -20.0f;
    private float incrementAmt = 2.0f;

    public float minZOff = -25.0f;
    public float maxZOff = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            // Check for zoom
            float scrollDelta = Input.mouseScrollDelta.y;

            if (scrollDelta != 0)
            {
                // Increment or decrement based on scroll direction
                if (scrollDelta > 0) {
                    if (zOffset < maxZOff)
                        zOffset += incrementAmt; // Scroll up => add value
                } else {
                    if (zOffset > minZOff)
                        zOffset -= incrementAmt; // Scroll down => subtract value
                }
            }

            // Set the camera's position to roughly match the player's position
            transform.position = new Vector3(player.position.x-2.5f, player.position.y, player.position.z + zOffset);
        }
    }
}
