using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidAudioSource : AudioSource
{
    void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag != "Bubble") {
            Play();
        }
    }
}
