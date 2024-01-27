using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Inputs
    public InputAction moveAction;
    public InputAction jumpAction;
    Vector2 moveValue;

    //Components
    public GameObject playerModel;
    Rigidbody rb;
    public GameObject camPivot;
    public GameObject cam;
    public LayerMask layerMask;

    //Variables
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float maxMagnitude;
    float targetXRotation;
    float targetYRotation;
    bool onGround = false;
    public float deccelRate = 1.1f;
    public float camDist = 5f;
    public float groundDist = 4;
    public Animator controller;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetXRotation = camPivot.transform.localRotation.x;
        targetYRotation = camPivot.transform.localRotation.y;
        Physics.gravity *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            moveValue = moveAction.ReadValue<Vector2>();
            onGround = Physics.Raycast(playerModel.transform.position, Vector3.down, groundDist);

            targetXRotation -= Input.GetAxis("Mouse Y") * rotateSpeed;
            targetYRotation += Input.GetAxis("Mouse X") * rotateSpeed;
            targetXRotation = Mathf.Clamp(targetXRotation, -90, 90);

            camPivot.transform.eulerAngles = new Vector3(targetXRotation, camPivot.transform.eulerAngles.y, camPivot.transform.eulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetYRotation, transform.eulerAngles.z);

            //Prevent camera from passing through walls
            RaycastHit hit;
            Ray ray = new Ray(camPivot.transform.position, cam.transform.position - camPivot.transform.position);
            Physics.Raycast(ray, out hit, camDist, layerMask);
            if (hit.collider == null)
            {
                cam.transform.position = camPivot.transform.position - camPivot.transform.forward * camDist;
            }
            else
            {
                cam.transform.position = hit.point + (-ray.direction.normalized * 0.3f);
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();

        if(jumpAction.IsPressed() && onGround)
        {
            controller.SetBool("Jumped", true);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(jumpPower * Vector3.up);
        } else
        {
            controller.SetBool("Jumped", false);
        }
    }

    public void Movement()
    {
        rb.AddRelativeForce(new Vector3(moveValue.x * moveSpeed, 0, moveValue.y * moveSpeed));
        if(moveValue.x == 0)
        {
            var localVelocity = transform.InverseTransformDirection(rb.velocity);
            localVelocity = new Vector3(localVelocity.x / deccelRate, localVelocity.y, localVelocity.z);
            rb.velocity = transform.TransformDirection(localVelocity);
        }
        if(moveValue.y == 0)
        {
            var localVelocity = transform.InverseTransformDirection(rb.velocity);
            localVelocity = new Vector3(localVelocity.x, localVelocity.y, localVelocity.z / deccelRate);
            rb.velocity = transform.TransformDirection(localVelocity);
        }
        if(new Vector2(rb.velocity.x, rb.velocity.z).magnitude > maxMagnitude)
        {
            var clampedVelocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x, rb.velocity.z), maxMagnitude);
            rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.y);
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }
}
