using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            rb.AddForce(new Vector3(speed,0,0));
        }
    }
}
