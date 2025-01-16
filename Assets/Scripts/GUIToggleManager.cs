using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ToggleManager : MonoBehaviour
{
    public static void ComponentsSetToggle(GameObject[] objs, bool isVisible) {
        float alpha = isVisible ? 1.00f : 0.00f; 

        foreach (GameObject obj in objs)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);
            
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = isVisible;
            }

            TextMeshProUGUI[] textMeshProUGUIs = obj.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI textMeshProUGUI in textMeshProUGUIs)
            {
                CanvasRenderer canvasRenderer = textMeshProUGUI.GetComponent<CanvasRenderer>();
                if (canvasRenderer != null)
                {
                    canvasRenderer.SetAlpha(alpha);
                }
            }

            Button[] buttons = obj.GetComponentsInChildren<Button>(true);
            foreach (Button btn in buttons)
            {
                CanvasRenderer btnRenderer = btn.GetComponent<CanvasRenderer>();
                btnRenderer.SetAlpha(alpha);
            }
        }
    }
}
