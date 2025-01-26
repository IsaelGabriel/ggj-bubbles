using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Button : Interactable, Signal
{
    [Header("Button Settings")]
    public float activationDuration = 3f;
    public Animator animator;

    private bool _isPressed = false;
    private float deactivationTime;

    [SerializeField]
    private AudioSource _audioSource;

    public bool Activated { 
        get => _isPressed; 
    }

    private void Update()
    {
        if (_isPressed && Time.time >= deactivationTime)
        {
            DeactivateButton();
        }
    }

    public void ActivateButton()
    {
        _isPressed = true;
        deactivationTime = Time.time + activationDuration;
        _audioSource.Play();
        if (animator != null)
        {
            animator.SetTrigger("Button");
        }
    }

    private void DeactivateButton()
    {
        _isPressed = false;
    }
    
    public override void OnInteract()
    {
        ActivateButton();
    }
}
