using PDollarGestureRecognizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DrawUIEvents : MonoBehaviour
{

    public Camera PlayerCamera;
    private UIDocument _document;
    private VisualElement root;
    private VisualElement drawArea;
    //private bool isDrawing = false;
    //public PlayerAbility will be used to connect the player's ability with the UI

    //Gesture Logic


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



    }


    void Update()
    {

    }
}

