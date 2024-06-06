using UnityEngine;
using UnityEngine.UI;

public class ObjectHider : MonoBehaviour
{
    [SerializeField] private Button hideButton; // UI Button
    [SerializeField] private GameObject targetObject; // The GameObject to hide

    void Start()
    {
        // Ensure the button and target object are assigned
        if (hideButton != null && targetObject != null)
        {
            // Add a listener to the button to call the HideObject method when clicked
            hideButton.onClick.AddListener(HideObject);
        }
        else
        {
            Debug.LogError("HideButton or TargetObject is not assigned in the Inspector.");
        }
    }

    // Method to hide the target object
    void HideObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
