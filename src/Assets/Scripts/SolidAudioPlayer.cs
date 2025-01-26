using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidAudioSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag != "Bubble") {
            _audioSource.Play();
        }
    }
}
