using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private Color colorRegular;
    private Color colorDark;
    private Color colorLight;
    private GameObject[] untoggleableObjects;
    private GameObject[] toggleableObjects;
    public static bool menuInteractable;
    public int r;
    public int g;
    public int b;
    public float a;
    private 

    // Start is called before the first frame update
    void Start()
    {
        colorRegular = new Color(r/255f, g/255f, b/255f, a);
        colorDark = new Color(((r-10 < 0) ? 0 : r-10)/255f, ((g-10 < 0) ? 0 : g-10)/255f, ((b-10 < 0) ? 0 : b-10)/255f, a);
        colorLight = new Color(((r+30 > 255) ? 255 : r+30)/255f, ((g+30 > 255) ? 255 : g+30)/255f, ((b+30 > 255) ? 255 : b+30)/255f, a);

        button = GetComponent<Button>();
        button.image.color = colorRegular;

        menuInteractable = false;

        // Set what should be invisible
        untoggleableObjects = GameObject.FindGameObjectsWithTag("Untoggleable");
        toggleableObjects = GameObject.FindGameObjectsWithTag("Toggleable");
        ToggleManager.ComponentsSetToggle(untoggleableObjects, false);
        ToggleManager.ComponentsSetToggle(toggleableObjects, true);
    }

    // Pointer Enter (Hover)
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = colorDark;
    }

    // Pointer Exit (Normal)
    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.color = colorRegular;
    }

    // Pointer Down (Button Pressed)
    public void OnPointerDown(PointerEventData eventData)
    {
        button.image.color = colorLight;

        if (button.CompareTag("Play")) {
            SceneManager.LoadScene("MainScene");
        } else if (button.CompareTag("Home")) {
            SceneManager.LoadScene("StartScene");
        } else if (button.CompareTag("Instruction")) {
            InterpretObjectTag("Back", "Back", false);
        } else if (button.CompareTag("Back")) {
            InterpretObjectTag("Instruction", "Instructions", true);
        } else if (button.CompareTag("Close")) {
            if (menuInteractable) {
                PlayerMovement.isCloseMenu = true;
                menuInteractable = false;
            }
        } else if (button.CompareTag("BuyUpgrade1")) {
            if (menuInteractable) {
                UpgradeManager.upgradeToBePurchased = 0;
                UpgradeManager.updateStatus = true;
            }
        } else if (button.CompareTag("BuyUpgrade2")) {
            if (menuInteractable) {
                UpgradeManager.upgradeToBePurchased = 1;
                UpgradeManager.updateStatus = true;
            }
        } else if (button.CompareTag("BuyUpgrade3")) {
            if (menuInteractable) {
                UpgradeManager.upgradeToBePurchased = 2;
                UpgradeManager.updateStatus = true;
            }
        } else if (button.CompareTag("BuyUpgrade4")) {
            if (menuInteractable) {
                UpgradeManager.upgradeToBePurchased = 3;
                UpgradeManager.updateStatus = true;
            }
        } else if (button.CompareTag("BuyUpgrade5")) {
            if (menuInteractable) {
                UpgradeManager.upgradeToBePurchased = 4;
                UpgradeManager.updateStatus = true;
            }
        } else {
            UpgradeManager.updateStatus = false;
        }
    }

    public void InterpretObjectTag(string tagName, string textName, bool isVisible)
    {
        button.gameObject.tag = tagName;
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = textName;

        ToggleManager.ComponentsSetToggle(toggleableObjects, isVisible);
        ToggleManager.ComponentsSetToggle(untoggleableObjects, !isVisible);
    }

    // Pointer Released (Hovered)
    public void OnPointerUp(PointerEventData eventData)
    {
        button.image.color = colorDark;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
