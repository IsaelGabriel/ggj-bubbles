using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : AudioSource
{
    public float force = 500f;

    void OnCollisionEnter(Collision collision) {
        Play();
        if(collision.gameObject.GetComponent<Rigidbody>() != null) {
            Vector3 contact = collision.GetContact(0).point;
            Vector3 forceDirection = contact - transform.position;
            
         1   collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force * forceDirection, contact, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }
}
