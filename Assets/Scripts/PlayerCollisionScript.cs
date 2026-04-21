using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool isGrounded;
    public static List<Renderer> scannableObjects = new List<Renderer>();
    public static List<Collider> bridgeObjects = new List<Collider>();
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Bridge"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Bridge"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
                //Debug.Log(other.tag);
        switch (other.tag)
        {
            case "Scannable":
                scannableObjects.Add(other.GetComponent<Renderer>());
                break;

            case "Bridge":
                bridgeObjects.Add(other.GetComponent<Collider>());
                break;
        }

        //Debug.Log(bridgeObjects.Count);

        
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.tag);

        switch (other.tag)
        {
            case "Scannable":
                scannableObjects.Remove(other.GetComponent<Renderer>());
                break;

            case "Bridge":
                bridgeObjects.Remove(other.GetComponent<Collider>());
                break;
        }
        //Debug.Log(bridgeObjects.Count);

    }
}
