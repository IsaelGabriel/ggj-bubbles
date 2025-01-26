using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SignalInteractable : Interactable
{
    public abstract bool Activated {
        get;
        protected set;
    }
}
