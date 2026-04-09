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
    private VisualElement drawArea;
    
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Debug.Log("ability script");
        drawUIRoot = DrawUI.GetComponentInChildren<UIDocument>().rootVisualElement;
        UnityEngine.Cursor.visible = true;
        //Debug.Log(drawUIRoot);
        initializeDrawUI();
        drawArea = drawUIRoot.Q("DrawArea");
        //Debug.Log(drawArea);
    }

    //For some reason the MainContainer Display starts as Null instead of None??
    private void initializeDrawUI() {
        drawUIRoot.Q("MainContainer").style.display = DisplayStyle.None;
    }


    public void Scan(InputAction.CallbackContext context) {

            //Debug.Log("used scan");
        if (canScan && context.performed)
        {
            foreach(Renderer scannableObject in PlayerCollisionScript.scannableObjects)
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
                UnityEngine.Cursor.visible = true;
            }else
            {
                mainContainer.style.display = DisplayStyle.None;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        drawArea.pickingMode = PickingMode.Position;
        drawArea.style.backgroundColor = Color.red;
    }

    
}
