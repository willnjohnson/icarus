using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Size and thickness of crosshair
    public int size = 16;
    public int thickness = 2;

    void Start()
    {
        // Generate and assign the crosshair texture
        Texture2D ct = GenerateCrosshair(size, thickness);
        Vector2 ctCenter = new Vector2(ct.width / 2, ct.height / 2);
        Cursor.SetCursor(ct, ctCenter, CursorMode.Auto);
    }

    Texture2D GenerateCrosshair(int size, int thickness)
    {
        // New texture
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        
        // Iterate through texture pixels
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (((x >= (size - thickness) / 2) && (x < (size + thickness) / 2)) || ((y >= (size - thickness) / 2) && (y < (size + thickness) / 2)))
                {
                    texture.SetPixel(x, y, Color.white);
                }
                else
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }

        // Apply and return texture
        texture.Apply();
        return texture;
    }
}
