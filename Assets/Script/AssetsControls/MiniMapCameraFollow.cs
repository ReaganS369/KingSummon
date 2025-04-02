using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    // Reference to the player's transform
    public Transform playerTransform;

    // Offset from the player's position to the minimap camera position
    public Vector3 offset;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned in the MinimapCameraFollow script.");
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Set the camera's position to the player's position plus the offset
            transform.position = playerTransform.position + offset;
        }
    }
}
