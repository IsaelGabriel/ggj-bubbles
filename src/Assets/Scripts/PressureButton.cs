using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour, Signal
{

    [SerializeField]
    private float _minimumActivationMass = 1f;

    private bool _activated = false;


    public bool Activated => _activated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision) {
        Rigidbody rigidbody = collision.transform.GetComponent<Rigidbody>();
        if(rigidbody != null) {
            if(rigidbody.mass >= _minimumActivationMass) _activated = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        Rigidbody rigidbody = collision.transform.GetComponent<Rigidbody>();
        if(rigidbody != null) {
            if(rigidbody.mass >= _minimumActivationMass) _activated = false;
        }
    }
}
