using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool isGrounded;
    public static List<Renderer> scannableObjects = new List<Renderer>();
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger");
        if (other.tag == "Scannable")
        {
            scannableObjects.Add(other.GetComponent<Renderer>());
            //Debug.Log(scannableObjects.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Scannable")
        {
            scannableObjects.Remove(other.GetComponent<Renderer>());
            //Debug.Log(scannableObjects.Count);
        }
    }
}
