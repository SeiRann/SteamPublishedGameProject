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
    public float horizontalSensitivity = 5f;

    public float lookSmoothTime = 1f;

    private Vector2 currentLook;
    private Vector2 lookVelocity;

    public float yaw;
    public float pitch;


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
        playerCamera.transform.rotation = playerCameraPoint.transform.rotation;
        player.Rotate(0, _mouseDirection.x/10, 0);
        Cursor.lockState = CursorLockMode.Locked;
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 move = (player.right * _moveDirection.x + player.forward * _moveDirection.y) * moveSpeed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

    }
}
