using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private CharacterController controller;
    private float xRotation = 0f;
    private float verticalVelocity = 0f;
    public float gravity = -9.81f;

    private AudioSource walkAudio;
    private float stepCooldown = 0.5f; // time between footstep sounds
    private float stepTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        walkAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Play footstep sound only when grounded and moving
        bool isMoving = horizontal != 0 || vertical != 0;
        if (controller.isGrounded && isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                walkAudio.PlayOneShot(walkAudio.clip);
                stepTimer = stepCooldown;
            }
        }
        else
        {
            stepTimer = 0f; // reset timer if not moving
        }

        // Gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // keep grounded
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        // Final move
        controller.Move(move * speed * Time.deltaTime);
    }
    public void FreezePlayer()
{
    enabled = false;
}

public void UnfreezePlayer()
{
    enabled = true;
}

}
