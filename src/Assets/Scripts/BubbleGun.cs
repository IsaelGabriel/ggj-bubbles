using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : MonoBehaviour
{

    [SerializeField]
    private GameObject _bubblePrefab;
    
    [SerializeField]
    private float _exitForce = 15f;

    [SerializeField]
    private Transform _exit;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            GameObject bubble = Instantiate(_bubblePrefab);
            bubble.transform.SetPositionAndRotation(_exit.position, _exit.rotation);
            bubble.GetComponent<Rigidbody>().AddForce(_exit.forward * _exitForce, ForceMode.Impulse);
        }
    }
}
