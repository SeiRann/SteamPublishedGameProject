using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TestScript : MonoBehaviour
{
    public bool canScan = true;
    public bool canDraw = true;
    public GameObject DrawUI;

    private VisualElement drawUIRoot;
    private VisualElement drawArea;

    void Awake()
    {
        drawUIRoot = DrawUI.GetComponentInChildren<UIDocument>().rootVisualElement;
        UnityEngine.Cursor.visible = true;
        //Debug.Log(drawUIRoot);
        initializeDrawUI();
        drawArea = drawUIRoot.Q("DrawArea");
        //Debug.Log(drawArea);

        

    }

    private void initializeDrawUI()
    {
        drawUIRoot.Q("MainContainer").style.display = DisplayStyle.None;
    }

    public void toggleDrawing(InputAction.CallbackContext context)
    {
        VisualElement mainContainer = drawUIRoot.Q("MainContainer");

        if (canDraw && context.performed)
        {
            Debug.Log("toggled drawing" + mainContainer.style.display);

            if (mainContainer.style.display == DisplayStyle.None)
            {

                mainContainer.style.display = DisplayStyle.Flex;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
                //playerController.cameraCanMove = false;

            }
            else
            {
                mainContainer.style.display = DisplayStyle.None;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                //playerController.cameraCanMove = true;
            }
        }
    }

}
