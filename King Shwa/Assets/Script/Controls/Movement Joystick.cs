using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    [SerializeField] private GameObject joystickBackground;
    [SerializeField] private GameObject joystick;
    public Vector2 joystickVec;

    private Vector2 joystickOriginalPosition;
    private Vector2 joystickTouchPosition;
    private float joystickScale;

    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalPosition = joystickBackground.transform.position;
        joystickScale = joystickBackground.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    //when the screen is pressed
    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBackground.transform.position = Input.mousePosition;
        joystickTouchPosition = Input.mousePosition;
    }

    //when drag on screen 
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPosition).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPosition);

        if (joystickDist < joystickScale)
        {
            joystick.transform.position = joystickTouchPosition + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPosition + joystickVec * joystickScale;
        }
    }

    //when the screen is not pressed
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPosition;
        joystickBackground.transform.position = joystickOriginalPosition;
    }
}
