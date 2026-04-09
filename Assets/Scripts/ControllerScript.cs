using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerScript : MonoBehaviour
{
    public bool cameraMovement = true; 

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

    public float sprintBoost;
    public float baseFOV;
    public float sprintFOV;

    private bool isSprinting=false;


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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Jump(InputAction.CallbackContext ctx) {
        //Debug.Log(collisionScript.isGrounded);
        if (collisionScript.isGrounded & ctx.performed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

    }


    public void Sprint(InputAction.CallbackContext ctx) { 
        //change FOV
        //make player go fast

        if (ctx.performed)
        {

            isSprinting = true;
            
        }

        if (ctx.canceled)
        {
            //go normal speed
            isSprinting = false;
            
        }
    }

    private void MoveCamera() {
        if (cameraMovement)
        {
            _mouseDirection = mouseReference.action.ReadValue<Vector2>();

            //Camera Positioning and Rotation
            playerCamera.transform.position = playerCameraPoint.transform.position;

            //// Base rotation from camera point
            Quaternion baseRotation = playerCameraPoint.transform.rotation;

            //// Mouse input
            float mouseX = _mouseDirection.x * horizontalSensitivity;
            float mouseY = _mouseDirection.y * horizontalSensitivity;



            //// Yaw (player rotates)
            player.Rotate(0, mouseX, 0);

            //// Pitch (camera only)
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            //// Combine base rotation + pitch
            playerCamera.transform.rotation = baseRotation * Quaternion.Euler(pitch, 0f, 0f);
        }
    }


    private void Update()
    {
        MoveCamera();

        _moveDirection = moveReference.action.ReadValue<Vector2>();
        
        

        //Camera FOV
        sprintFOV = Mathf.Clamp(sprintFOV, baseFOV, sprintFOV);
        if (isSprinting && playerCamera.fieldOfView < sprintFOV)
        {
            playerCamera.fieldOfView += 0.5f;
        } else if (playerCamera.fieldOfView > baseFOV)
        {
            playerCamera.fieldOfView -= 0.5f;
        }
        

    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(isSprinting);
        if (!isSprinting)
        {
            Vector3 move = (player.right * _moveDirection.x + player.forward * _moveDirection.y) * moveSpeed;
            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        } else
        {
            Vector3 move = (player.right * _moveDirection.x + player.forward * _moveDirection.y) * moveSpeed*sprintBoost;
            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        }

    }

    
}
