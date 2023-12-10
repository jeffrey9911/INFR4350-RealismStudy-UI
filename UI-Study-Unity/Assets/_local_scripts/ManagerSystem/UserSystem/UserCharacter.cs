using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCharacter : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 120f;
    public float gravity = 9.81f;

    public Transform HeadForwardDirection;

    private CharacterController characterController;

    private Vector2 LeftJoystick;
    private Vector2 RightJoystick;
    private Vector3 verticalVelocity = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        LeftJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        RightJoystick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        //LeftJoystick.x = Input.GetAxis("Horizontal");
        //LeftJoystick.y = Input.GetAxis("Vertical");


        Vector3 movement = (HeadForwardDirection.forward * LeftJoystick.y + HeadForwardDirection.right * LeftJoystick.x) * moveSpeed * Time.deltaTime;

        verticalVelocity.y -= gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            verticalVelocity = Vector3.zero;
        }


        movement.y = 0f;

        movement += verticalVelocity * Time.deltaTime;

        
        characterController.Move(movement);


        Vector3 rotation = new Vector3(0f, RightJoystick.x, 0f) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation);
    }
}
