using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Inputs
    public InputAction moveAction;
    public InputAction jumpAction;
    public Vector2 moveValue;

    //Components
    public GameObject playerModel;
    Rigidbody rb;
    public GameObject camPivot;

    //Variables
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public float maxMagnitude;
    float targetXRotation;
    float targetYRotation;
    bool onGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetXRotation = camPivot.transform.localRotation.x;
        targetYRotation = camPivot.transform.localRotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            moveValue = moveAction.ReadValue<Vector2>();
            onGround = Physics.Raycast(playerModel.transform.position, Vector3.down, 1.5f);

            targetXRotation -= Input.GetAxis("Mouse Y") * rotateSpeed;
            targetYRotation += Input.GetAxis("Mouse X") * rotateSpeed;
            targetXRotation = Mathf.Clamp(targetXRotation, -90, 90);

            camPivot.transform.eulerAngles = new Vector3(targetXRotation, camPivot.transform.eulerAngles.y, camPivot.transform.eulerAngles.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetYRotation, transform.eulerAngles.z);
        }
    }
}
