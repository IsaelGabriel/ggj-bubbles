using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    public float force = 500f;

    void OnCollisionEnter(Collision collision) {
        _audioSource.Play();
        if(collision.gameObject.GetComponent<Rigidbody>() != null) {
            Vector3 contact = collision.GetContact(0).point;
            Vector3 forceDirection = contact - transform.position;
            
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force * forceDirection, contact, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }
}
