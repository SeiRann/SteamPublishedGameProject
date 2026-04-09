using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerScript : MonoBehaviour
{
    private Rigidbody rb;
    private Transform player;

    private Camera playerCamera;
    public GameObject playerCameraPoint;

    private Vector2 _moveDirection;
    private Vector2 _mouseDirection;
    public float moveSpeed = 1f;
    public float jumpForce = 5f;
    private PlayerCollisionScript collisionScript;
    public float horizontalSensitivity = 0.0001f;

    public float lookSmoothTime = 1f;

    private Vector2 currentLook;
    private Vector2 lookVelocity;

    private float pitch;
    public float maxPitch;
    public float minPitch;  


    public InputActionReference moveReference;
    public InputActionReference mouseReference;

    private void Awake()
    {
        player = transform.Find("Player");
        rb = player.GetComponent<Rigidbody>();
        //Debug.Log(rb);
        playerCamera = GetComponentInChildren<Camera>();
        collisionScript = GetComponentInChildren<PlayerCollisionScript>();
        
        

    }

    public void Jump(InputAction.CallbackContext ctx) {
        //Debug.Log(collisionScript.isGrounded);
        if (collisionScript.isGrounded & ctx.performed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

    }


   

    private void Update()
    {
        _moveDirection = moveReference.action.ReadValue<Vector2>();
        _mouseDirection = mouseReference.action.ReadValue<Vector2>();

        //Camera Positioning and Rotation
        playerCamera.transform.position = playerCameraPoint.transform.position;
        
        //// Base rotation from camera point
        Quaternion baseRotation = playerCameraPoint.transform.rotation;

        //// Mouse input
        float mouseX = _mouseDirection.x * horizontalSensitivity;
        float mouseY = _mouseDirection.y * horizontalSensitivity;

        Debug.Log(mouseX);
        Debug.Log(horizontalSensitivity);

        //// Yaw (player rotates)
        player.Rotate(0, mouseX, 0);

        //// Pitch (camera only)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        //// Combine base rotation + pitch
        playerCamera.transform.rotation = baseRotation * Quaternion.Euler(pitch, 0f, 0f);
        //playerCamera.transform.Rotate(_mouseDirection.y, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 move = (player.right * _moveDirection.x + player.forward * _moveDirection.y) * moveSpeed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

    }
}
