using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animator animator;
    private bool isOpen = false;
    
    [Header("Door Settings")]
    // if debug mode is on, normal input is ignored
    public bool debugMode = false;

    [Header("Door Inputs")]
    public List<Signal> doorInputs;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No animator attached to this door! Object name: " + gameObject.name);
        }
    }

    private void Update()
    {
        if (!debugMode)
        {
            bool allInputsActive = AreAllInputsActive();
            if (allInputsActive && !isOpen)
            {
                OpenDoor();
            }
            else if (!allInputsActive && isOpen)
            {
                CloseDoor();
            }
        }
    }
    
    public override void OnInteract()
    {

        if (!debugMode)
        {
            return;
        }

        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private bool AreAllInputsActive()
    {
        foreach (Signal doorInput in doorInputs)
        {
            if (!doorInput.Activated)
            {
                return false;
            }
        }
        return true;
    }
    
    public void OpenDoor()
    {
        isOpen = true;
        animator.Play("door open");
    }

    public void CloseDoor()
    {
        isOpen = false;
        animator.Play("door close");
    }
}
