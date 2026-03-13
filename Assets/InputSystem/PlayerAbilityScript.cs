using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAbilityScript : MonoBehaviour
{
    public bool canScan = true;
    public bool canDraw = true;
    public static List<Renderer> scannableObjects = new List<Renderer>();
    UIDocument DrawGui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DrawGui = GetComponent<UIDocument>();
        
    }

    public void Scan(InputAction.CallbackContext context) {

        if (canScan && context.performed)
        {
            Debug.Log("used scan");
            foreach(Renderer scannableObject in scannableObjects)
            {
                scannableObject.material.color = Color.yellow;
            }
        }
    }

    public void toggleDrawing() {
        if (canDraw) {
            DrawGui.enabled = !DrawGui.enabled;
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
        
    }
}
