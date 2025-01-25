using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _acceleration = 100f;
    [SerializeField] 
    private float _deceleration = 100f;
    [SerializeField]
    private float _maxVelocity = 50f;


    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {

    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void Move() {
        Vector3 input = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);

        Vector3 currentVelocity = _rigidbody.velocity;
        currentVelocity.y = 0f;


        currentVelocity = currentVelocity.normalized * Math.Clamp(currentVelocity.magnitude - (_deceleration * Time.deltaTime), 0f, currentVelocity.magnitude);


        if(input.magnitude != 0f) {
            if(currentVelocity.magnitude <= _maxVelocity) {
                currentVelocity += input * _acceleration * Time.deltaTime;
                currentVelocity = Vector3.ClampMagnitude(currentVelocity, _maxVelocity);
            }else {
                Vector3 newVelocity = currentVelocity + input * _acceleration * Time.deltaTime;
                if(Math.Abs(newVelocity.x) < Math.Abs(currentVelocity.x)) currentVelocity.x = newVelocity.x;
                if(Math.Abs(newVelocity.y) < Math.Abs(currentVelocity.y)) currentVelocity.y = newVelocity.y;
            }
        }

        currentVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = currentVelocity;

    }
}
