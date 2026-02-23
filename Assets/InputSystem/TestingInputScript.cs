using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputScript : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context) {
        Debug.Log("Jump " + context.phase);
        if (context.phase == InputActionPhase.Performed)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    public void Forward(InputAction.CallbackContext context)
    {
        Debug.Log("Forward");
        if (context.phase == InputActionPhase.Performed)
        {
            rb.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
        }
    }
}
