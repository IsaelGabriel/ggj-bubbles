using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : DoorInput
{
    [Header("Button Settings")]
    public float activationDuration = 3f;
    public Animator animator;

    private bool isPressed = false;
    private float deactivationTime;

    private void Update()
    {
        if (isPressed && Time.time >= deactivationTime)
        {
            DeactivateButton();
        }
    }

    public override bool IsActivated()
    {
        return isPressed;
    }

    public void ActivateButton()
    {
        if (isPressed) return;

        isPressed = true;
        deactivationTime = Time.time + activationDuration;
        if (animator != null)
        {
            animator.SetTrigger("button");
        }
        Debug.Log("button pressed");
    }

    private void DeactivateButton()
    {
        isPressed = false;
        Debug.Log("button deactivated");
    }
    
    public override void OnInteract()
    {
        ActivateButton();
    }
}
