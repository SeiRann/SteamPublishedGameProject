using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;



public class PlayerAbilityScript : MonoBehaviour
{
    public bool canScan = true;
    public bool canDraw = true;
    public GameObject DrawUI;

    private VisualElement drawUIRoot;

    public static List<Renderer> scannableObjects = new List<Renderer>();
    FirstPersonController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        drawUIRoot = DrawUI.GetComponentInChildren<UIDocument>().rootVisualElement;
        initializeDrawUI();
 
        playerController = GetComponent<FirstPersonController>();

    }

    //For some reason the MainContainer Display starts as Null instead of None??
    private void initializeDrawUI() {
        drawUIRoot.Q("MainContainer").style.display = DisplayStyle.None;
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
        VisualElement mainContainer = drawUIRoot.Q("MainContainer");

        if (canDraw && context.performed) {
            Debug.Log("toggled drawing" + mainContainer.style.display);

            if (mainContainer.style.display == DisplayStyle.None) {

                mainContainer.style.display = DisplayStyle.Flex;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                playerController.cameraCanMove = false;

            }else
            {
                mainContainer.style.display = DisplayStyle.None;
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
