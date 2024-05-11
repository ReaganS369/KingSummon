using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] private GameObject upObject;
    [SerializeField] private GameObject downObject;
    [SerializeField] private GameObject leftObject;
    [SerializeField] private GameObject rightObject;
    [SerializeField] private GameObject upperLeftObject;
    [SerializeField] private GameObject upperRightObject;
    [SerializeField] private GameObject lowerLeftObject;
    [SerializeField] private GameObject lowerRightObject;

    [SerializeField] private Rigidbody2D playerRigidbody;

    private GameObject currentActiveObject;

    public Rigidbody2D PlayerRigidbody
    {
        get { return playerRigidbody; }
    }

    // Event to notify when the direction changes
    public event System.Action<Vector2> OnDirectionChange;

    void Update()
    {
        Vector2 movement = playerRigidbody.velocity;

        if (movement.sqrMagnitude > 0)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360;
            }

            GameObject newActiveObject = GetDirectionObject(angle);

            if (newActiveObject != currentActiveObject)
            {
                if (currentActiveObject != null)
                {
                    currentActiveObject.SetActive(false);
                }

                if (newActiveObject != null)
                {
                    newActiveObject.SetActive(true);
                }

                currentActiveObject = newActiveObject;

                // Notify subscribers about the direction change
                OnDirectionChange?.Invoke(movement.normalized);
            }

            FlipCharacterOrientationBasedOnDirection(angle, currentActiveObject);
        }
    }

    GameObject GetDirectionObject(float angle)
    {
        if (angle >= 337.5f || angle < 22.5f)
        {
            return rightObject;
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            return upperRightObject;
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            return upObject;
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            return upperLeftObject;
        }
        else if (angle >= 157.5f && angle < 202.5f)
        {
            return leftObject;
        }
        else if (angle >= 202.5f && angle < 247.5f)
        {
            return lowerLeftObject;
        }
        else if (angle >= 247.5f && angle < 292.5f)
        {
            return downObject;
        }
        else if (angle >= 292.5f && angle < 337.5f)
        {
            return lowerRightObject;
        }

        return null;
    }

    void FlipCharacterOrientationBasedOnDirection(float angle, GameObject activeObject)
    {
        if (angle >= 112.5f && angle < 247.5f)
        {
            activeObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            activeObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
