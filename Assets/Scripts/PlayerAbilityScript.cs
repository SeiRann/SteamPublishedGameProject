using PDollarGestureRecognizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;



public class PlayerAbilityScript : MonoBehaviour
{
    public ControllerScript controllerScript;
    public Camera PlayerCamera;

    public bool canScan = true;
    public bool canDraw = true;
    public GameObject DrawUI;

    private VisualElement drawUIRoot;
    private VisualElement drawArea;

    private bool isDrawing;

    // Gesture Logic
    public Transform gestureOnScreenPrefab; //The line renderer used to draw lines
    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    //Logic

    private List<Gesture> trainingSet = new List<Gesture>();
    private Vector3 virtualKeyPosition = Vector2.zero;

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private int vertexCount = 0;
    private bool recognized;

    private bool mouseInDrawArea;
    private string newGestureName;

    private RuntimePlatform platform;

    private void Start()
    {
        platform = Application.platform;

        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }


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
    private void initializeDrawUI()
    {
        drawUIRoot.Q("MainContainer").style.display = DisplayStyle.None;
    }


    public void Scan(InputAction.CallbackContext context)
    {

        //Debug.Log("used scan");
        if (canScan && context.performed)
        {
            foreach (Renderer scannableObject in PlayerCollisionScript.scannableObjects)
            {
                scannableObject.material.color = Color.yellow;
            }
        }
    }

    public void toggleDrawing(InputAction.CallbackContext context)
    {
        VisualElement mainContainer = drawUIRoot.Q("MainContainer");

        if (canDraw && context.performed)
        {
            //Debug.Log("toggled drawing" + mainContainer.style.display);

            if (mainContainer.style.display == DisplayStyle.None)
            {
                isDrawing = true;
                controllerScript.cameraMovement = false;
                mainContainer.style.display = DisplayStyle.Flex;
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
            }
            else
            {
                ResetGestures();
                isDrawing = false;
                controllerScript.cameraMovement = true;
                mainContainer.style.display = DisplayStyle.None;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
            }
        }
    }


    private void ResetGestures()
    {
        recognized = false;
        strokeId = -1;

        points.Clear();

        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
        {

            lineRenderer.positionCount = 0;
            Destroy(lineRenderer.gameObject);
        }

        gestureLinesRenderer.Clear();
    }

    private void RenderAndAddLine()
    {
        Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
        currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

        gestureLinesRenderer.Add(currentGestureLineRenderer);
    }

    private void AddAndResetPoint()
    {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

        //Debug.Log(currentGestureLineRenderer);
        currentGestureLineRenderer.positionCount = ++vertexCount;
        currentGestureLineRenderer.SetPosition(vertexCount - 1, PlayerCamera.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
    }

    private void SetupPlatformTouch()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
    }


    public void Recognize(InputAction.CallbackContext ctx)
    {

        Debug.Log("Recognize");
        if (ctx.performed)
        {
            recognized = true;

            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);
        }
    }

    public void AddGesture(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && points.Count > 0 && newGestureName != "")
        {
            string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

#if !UNITY_WEBPLAYER
            GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
#endif

            trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

            newGestureName = "";
        }
    }



    // Update is called once per frame
    void Update()
    {
        //drawArea.pickingMode = PickingMode.Position;
        //drawArea.style.backgroundColor = Color.red;

        SetupPlatformTouch();

        if (isDrawing)
        {

            if (Input.GetMouseButtonDown(0))    //Check if it was pressed
            {
                if (recognized)
                {
                    ResetGestures();    //If the gesture got recognize reset the canvas
                }

                ++strokeId;     //Add another stroke identifier

                RenderAndAddLine(); //Draw the line and add it to the gesture set

                vertexCount = 0; //Reset the vertex count
            }

            if (Input.GetMouseButton(0)) //If the mouse is pressed
            {
                AddAndResetPoint(); // just add points and then count its positioncount
            }

        }


    }
}
