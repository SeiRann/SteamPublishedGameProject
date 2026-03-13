using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using System.Net;
#endif

public class PlayerAbilityScript : MonoBehaviour
{
    public bool canScan = true;
    public bool canDraw = true;
    public static List<Renderer> scannableObjects = new List<Renderer>();
    UIDocument DrawGui;
    FirstPersonController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DrawGui = GetComponent<UIDocument>();
        Debug.Log(DrawGui);
        playerController = GetComponent<FirstPersonController>();
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

    public void toggleDrawing(InputAction.CallbackContext context) {

        if (canDraw && context.performed) {
            Debug.Log("toggled drawing");   

            DrawGui.enabled = !DrawGui.enabled;

            if(DrawGui.enabled == true)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                playerController.cameraCanMove = false;
            }
            else
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                playerController.cameraCanMove = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Scannable")
        {
            scannableObjects.Add(other.GetComponent<Renderer>());
            //Debug.Log(scannableObjects.Count);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Scannable")
        {
            scannableObjects.Remove(other.GetComponent<Renderer>());
            //Debug.Log(scannableObjects.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
