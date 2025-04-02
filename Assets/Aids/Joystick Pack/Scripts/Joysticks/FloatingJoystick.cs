using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    // Store the original position of the joystick
    private Vector2 originalPosition;

    protected override void Start()
    {
        base.Start();
        // Store the original position of the background
        originalPosition = background.anchoredPosition;
        // Keep the background active at the start
        // background.gameObject.SetActive(true); // No need to change the active state here since it should already be active
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // Set the joystick position to the touch point
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // Reset the joystick position to the original position
        background.anchoredPosition = originalPosition;
        base.OnPointerUp(eventData);
    }
}
