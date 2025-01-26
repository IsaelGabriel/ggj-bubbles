using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Player") {
            GameManager.NextLevel();
            Destroy(gameObject);
        }
    }
}
