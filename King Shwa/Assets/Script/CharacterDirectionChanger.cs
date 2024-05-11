using UnityEngine;

public class CharacterMovementChanger : MonoBehaviour
{
    // SerializeField to hold references to each directional GameObject
    [SerializeField] private GameObject upCharacter;
    [SerializeField] private GameObject upRightCharacter;
    [SerializeField] private GameObject rightCharacter;
    [SerializeField] private GameObject downRightCharacter;
    [SerializeField] private GameObject downCharacter;
    [SerializeField] private GameObject downLeftCharacter;
    [SerializeField] private GameObject leftCharacter;
    [SerializeField] private GameObject upLeftCharacter;

    // Method to update direction and activate the corresponding character
    public void UpdateDirection(Vector2 movementDirection)
    {
        // Deactivate all characters initially
        DeactivateAllCharacters();

        // Determine the closest direction and activate the corresponding character
        if (movementDirection == Vector2.zero)
        {
            // If no movement, don't activate any character
            return;
        }

        // Calculate the angle of the movement direction (in degrees)
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;

        // Activate the corresponding character based on the angle
        if (angle >= -22.5f && angle <= 22.5f)
        {
            rightCharacter.SetActive(true);
        }
        else if (angle > 22.5f && angle <= 67.5f)
        {
            downRightCharacter.SetActive(true);
        }
        else if (angle > 67.5f && angle <= 112.5f)
        {
            downCharacter.SetActive(true);
        }
        else if (angle > 112.5f && angle <= 157.5f)
        {
            downLeftCharacter.SetActive(true);
        }
        else if (angle > 157.5f || angle <= -157.5f)
        {
            leftCharacter.SetActive(true);
        }
        else if (angle > -157.5f && angle <= -112.5f)
        {
            upLeftCharacter.SetActive(true);
        }
        else if (angle > -112.5f && angle <= -67.5f)
        {
            upCharacter.SetActive(true);
        }
        else if (angle > -67.5f && angle <= -22.5f)
        {
            upRightCharacter.SetActive(true);
        }
    }

    // Method to deactivate all characters
    private void DeactivateAllCharacters()
    {
        upCharacter.SetActive(false);
        upRightCharacter.SetActive(false);
        rightCharacter.SetActive(false);
        downRightCharacter.SetActive(false);
        downCharacter.SetActive(false);
        downLeftCharacter.SetActive(false);
        leftCharacter.SetActive(false);
        upLeftCharacter.SetActive(false);
    }
}
