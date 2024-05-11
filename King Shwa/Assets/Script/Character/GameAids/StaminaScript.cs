using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    [SerializeField] private int maxStamina = 10; // Maximum stamina blocks
    [SerializeField] private float spacing = 10f; // Spacing between each stamina block in pixels
    [SerializeField] private GameObject staminaBlockPrefab; // Prefab of a single stamina block
    [SerializeField] private GameObject existingStaminaBlock; // Reference to the existing stamina block
    [SerializeField] private Button slideSkillButton; // Reference to the UI slide skill button

    private int currentStamina; // Current stamina value
    private GameObject[] staminaBlocks; // Array to hold references to the stamina block objects

    public int CurrentStamina // Public property to access currentStamina
    {
        get { return currentStamina; }
        private set { currentStamina = value; }
    }

    public delegate void SlideSkillDelegate();
    public static event SlideSkillDelegate SlideSkillEvent;

    private void Start()
    {
        // Initialize the current stamina to maximum at the start
        CurrentStamina = maxStamina;

        // Initialize the stamina blocks array
        staminaBlocks = new GameObject[maxStamina];

        // Add the existing stamina block to the array
        staminaBlocks[0] = existingStaminaBlock;

        // Instantiate the remaining stamina blocks and position them
        for (int i = 1; i < maxStamina; i++)
        {
            // Instantiate a new stamina block
            GameObject newBlock = Instantiate(staminaBlockPrefab, existingStaminaBlock.transform.parent);

            // Position the new block to the left of the existing block with spacing
            Vector3 newPosition = existingStaminaBlock.transform.position;
            newPosition.x -= (existingStaminaBlock.GetComponent<RectTransform>().rect.width + spacing) * i;
            newBlock.transform.position = newPosition;

            // Store the new block in the array
            staminaBlocks[i] = newBlock;
        }

        // Assign the OnSlideSkillButtonPressed method to the slide skill button's onClick event
        slideSkillButton.onClick.AddListener(OnSlideSkillButtonPressed);

        // Update the UI to reflect the initial stamina value
        UpdateStaminaUI();
    }

    // This method is called when the slide skill button is pressed.
    private void OnSlideSkillButtonPressed()
    {
        // Trigger the slide skill event
        SlideSkillEvent?.Invoke();
    }

    // Reduces the current stamina by the specified amount.
    public void ReduceStamina(int amount)
    {
        CurrentStamina -= amount;
        if (CurrentStamina < 0)
            CurrentStamina = 0;
        UpdateStaminaUI();
    }

    // Increases the current stamina by the specified amount.
    public void IncreaseStamina(int amount)
    {
        CurrentStamina += amount;
        if (CurrentStamina > maxStamina)
            CurrentStamina = maxStamina;
        UpdateStaminaUI();
    }

    // Updates the visual representation of the stamina blocks in the UI.
    private void UpdateStaminaUI()
    {
        // Loop through each stamina block
        for (int i = 0; i < maxStamina; i++)
        {
            // If the current index is less than the current stamina, enable the block
            if (i < CurrentStamina)
            {
                staminaBlocks[i].SetActive(true);
            }
            // Otherwise, disable the block
            else
            {
                staminaBlocks[i].SetActive(false);
            }
        }
    }
}
