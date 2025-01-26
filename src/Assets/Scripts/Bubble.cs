using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float force = 500f;

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.GetComponent<Rigidbody>() != null) {
            Vector3 contact = collision.GetContact(0).point;
            Vector3 forceDirection = contact - transform.position;
            
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force * forceDirection, contact, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }
}
