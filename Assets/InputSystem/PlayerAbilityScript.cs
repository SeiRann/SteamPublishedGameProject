using System.Collections.Generic;

using UnityEngine;

public class PlayerAbilityScript : MonoBehaviour
{
    public bool scanAbility = true;
    public static List<Renderer> scannableObjects = new List<Renderer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SphereCollider sc = GetComponent<SphereCollider>();
        
        
    }

    public void Scan() {
        if (scanAbility)
        {
            foreach(Renderer scannableObject in scannableObjects)
            {
                scannableObject.material.color = Color.yellow;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Scannable")
        {
            scannableObjects.Add(other.GetComponent<Renderer>());
            Debug.Log(scannableObjects.Count);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Scannable")
        {
            scannableObjects.Remove(other.GetComponent<Renderer>());
            Debug.Log(scannableObjects.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Scan();
        }
    }
}
