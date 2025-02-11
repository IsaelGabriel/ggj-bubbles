using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float _acceleration = 100f;
    [SerializeField]
    private float _ungroundedAccelerationMultiplier = 0.1f;
    [SerializeField] 
    private float _deceleration = 100f;
    [SerializeField]
    private float _maxVelocity = 50f;


    [Header("Jump")]
    [SerializeField]
    private float _jumpForce = 100f;

    [SerializeField]
    private Transform bottom;


    [Header("View")]
    [SerializeField]
    private float mouseSensitivity = 500f;
    [SerializeField]
    private Transform headTransform;
    [SerializeField]
    private Vector2 headRange = new Vector2(-90f, 45f);
    private float headRotation = 0f;

    [Header("Interaction")]
    public Transform interactionLimitTransform;
    
    [HideInInspector]
    public Rigidbody pickedObject = null;
    
    [SerializeField]
    private float pickupForce = 150f;


    private Rigidbody _rigidbody;

    private bool _grounded = true;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update() {
        Jump();
        MoveHead();
        HandleInteraction();
    }
    
    void FixedUpdate()
    {
        CheckGrounded();

        Move();
    }

    private void Move() {
        Vector3 input = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);

        input = Quaternion.FromToRotation(Vector3.forward, transform.forward) * input;

        if(!_grounded) input *= _ungroundedAccelerationMultiplier;

        Vector3 currentVelocity = _rigidbody.velocity;
        currentVelocity.y = 0f;

        if(input.magnitude != 0f && currentVelocity.magnitude <= _maxVelocity) {
            currentVelocity += input * _acceleration * Time.deltaTime;
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, _maxVelocity);
        } else {
            if(_grounded) {
                currentVelocity = currentVelocity.normalized * Math.Clamp(currentVelocity.magnitude - (_deceleration * Time.deltaTime), 0f, currentVelocity.magnitude);
            }
            Vector3 newVelocity = currentVelocity + input * _acceleration * Time.deltaTime;
            if(Math.Abs(newVelocity.x) < Math.Abs(currentVelocity.x)) currentVelocity.x = newVelocity.x;
            if(Math.Abs(newVelocity.z) < Math.Abs(currentVelocity.z)) currentVelocity.z = newVelocity.z;

        }

        currentVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = currentVelocity;

    }
    private void Jump() {
        if(Input.GetButtonDown("Jump") && _grounded) {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void CheckGrounded() {
        RaycastHit hit;

        if(Physics.Raycast(bottom.position, Vector3.down, out hit)) {
            _grounded = hit.distance <= 0.05f;
        }
    }

    private void MoveHead() {
        Vector2 viewRotation = new Vector2(){
            x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime,
            y = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime
        };

        transform.Rotate(Vector3.up * viewRotation.x);

        headRotation += viewRotation.y;
        headRotation = Mathf.Clamp(headRotation + viewRotation.y, headRange.x, headRange.y);

        headTransform.localEulerAngles = new(headRotation, 0f, 0f);

    }

    private void HandleInteraction() {
        Vector3 pickedObjectVelocity = Vector3.zero;
        if(pickedObject != null) {
            MovePickedObject();
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            if(pickedObject != null) {
                DropObject();
                return;
            }
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            Vector3 rayDirection = interactionLimitTransform.position - headTransform.position;
            if(Physics.Raycast(ray, out hit, rayDirection.magnitude) && hit.transform.tag == "Interactable") {
                hit.transform.GetComponentInParent<Interactable>().OnInteract();
            }
        }
    }

    public void PickupObject(Rigidbody pickable) {

        pickable.useGravity = false;
        pickable.drag = 10f;
        pickable.constraints = RigidbodyConstraints.FreezeRotation;

        //pickable.transform.parent = interactionLimitTransform;

        pickedObject = pickable;
    }

    public void DropObject() {
        pickedObject.useGravity = true;
        pickedObject.drag = 1f;
        pickedObject.constraints = RigidbodyConstraints.None;

        //pickedObject.transform.parent = null;
        pickedObject = null;
    }

    private void MovePickedObject() {
        if(Vector3.Distance(pickedObject.transform.position, interactionLimitTransform.position) > 0.1f) {
            Vector3 moveDirection = interactionLimitTransform.position - pickedObject.transform.position;
            pickedObject.AddForce(moveDirection * pickupForce);
        }
    }
}
