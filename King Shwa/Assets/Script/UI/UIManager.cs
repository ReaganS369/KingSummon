using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements; // Array of UI elements to hide
    [SerializeField] private Button actionButton; // Reference to the button to show

    // Method to hide all specified UI elements
    public void HideUIElements()
    {
        foreach (var uiElement in uiElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }

    // Method to show the button
    public void ShowButton()
    {
        if (actionButton != null)
        {
            actionButton.gameObject.SetActive(true);
        }
    }
}
