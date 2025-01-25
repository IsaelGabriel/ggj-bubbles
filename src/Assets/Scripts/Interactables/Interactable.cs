using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactMessage = "Interact";
    public string stopInteractionMessage = "Stop interacting";

    public virtual void OnInteract() {}
}
