using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float walkSpeed = 5f, sprintSpeed = 10f, gravityForce = 9.8f, jumpForce = 5f;
    private float yVelocity;
    private CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // ?? Solo el jugador dueño procesa input
        if (!IsOwner) return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * x + transform.forward * z;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        movement *= currentSpeed;
        // Salto
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
            {
                yVelocity = -2f; // Mantener pegado al suelo
            }
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
            }
        }
        // Gravedad
        yVelocity -= gravityForce * Time.deltaTime;
        movement.y = yVelocity;
        controller.Move(movement * Time.deltaTime);

    }
}
