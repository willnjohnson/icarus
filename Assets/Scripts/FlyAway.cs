using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object travels in X direction and slowly fades (fading effect gives natural feel that object is disappearing) as reference object moves away
//   Reason for fade effect instead of wraparound trick is that player is moving in the Z direction, so the object may never go "off-screen"
//   NOTE: Set material's Rendering Mode to Transparent for this effect to be applied
public class FlyAway : MonoBehaviour
{
    public Transform player;

    public float speed = -5.0f;
    public float fadeDistance = 25.0f;

    private Renderer[] renderers;
    private Material[][] materials;
    private Color[][] startColors;

    // Start is called before the first frame update
    void Start()
    {
        // Set up defaults
        renderers = GetComponentsInChildren<Renderer>();
        PlayerMovement.enemyFade = 0.0f;

        materials = new Material[renderers.Length][];
        startColors = new Color[renderers.Length][];

        for (int i=0; i < renderers.Length; i++) {
            Renderer r = renderers[i];
            materials[i] = r.materials;
            startColors[i] = new Color[materials[i].Length];

            for (int j=0; j < materials[i].Length; j++)
            {
                startColors[i][j] = materials[i][j].color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move object in X direction
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Calculate how far displaced object is in Z direction to reference object
        float zDisplace = Mathf.Abs(transform.position.z - player.position.z);

        // Fade level is determined by the displacement, normalized to Clamp range (0,1) with 1.0 representing fully faded
        PlayerMovement.enemyFade = Mathf.Clamp01((zDisplace - fadeDistance) / fadeDistance);

        // Apply this fade
        Fade(PlayerMovement.enemyFade);
    }

    // Fade every material of child components by some level determined by enemyFade value
    void Fade(float fadeAmount) {
        for (int i=0; i<renderers.Length; i++) {
            for (int j=0; j<materials[i].Length; j++) {
                Color c = startColors[i][j];
                Color newColor = new Color(c.r, c.g, c.b, 1 - fadeAmount);

                materials[i][j].color = newColor;
            }
        }
    }
}