using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawUIEvents : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement root;
    private VisualElement drawArea;
    //private bool isDrawing = false;
    //public PlayerAbility will be used to connect the player's ability with the UI

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        root = _document.rootVisualElement;
        drawArea = root.Q("DrawArea");
        //Debug.Log(root);
        //Debug.Log(drawArea);
    }

    private void Start()
    { 
        drawArea.RegisterCallback<MouseEnterEvent>(evt =>
        {
            Debug.Log("Mouse entered");
        });

        drawArea.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            Debug.Log("Mouse exited");
        });
    }



}
