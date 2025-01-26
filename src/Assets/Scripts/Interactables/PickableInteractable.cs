using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableInteractable : Interactable
{    

    public override void OnInteract()
    {
        FindObjectOfType<PlayerController>().PickupObject(GetComponent<Rigidbody>());
    }


}
