using UnityEngine;

public class CharacterSizeAdjuster : MonoBehaviour
{
    [SerializeField] private Transform character; // Reference to your character GameObject
    private float screenHeight;

    private void Start()
    {
        screenHeight = Screen.height; // Get the screen height
        UpdateCharacterSize();
    }

    private void UpdateCharacterSize()
    {
        // Calculate the desired size (height of the screen)
        float desiredSize = screenHeight;

        // Calculate the aspect ratio of the character's current size
        float aspectRatio = character.localScale.y != 0 ? character.localScale.x / character.localScale.y : 1;

        // Adjust the character's width and height based on the screen height and aspect ratio
        character.localScale = new Vector3(desiredSize * aspectRatio, desiredSize, character.localScale.z);
    }
}
