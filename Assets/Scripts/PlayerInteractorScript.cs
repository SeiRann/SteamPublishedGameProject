using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractorScript : MonoBehaviour
{
    public ControllerScript controller;


    // TODO Replace these types of definitions with inheritance
    public Transform cameraTransform;

    public static float interactRange = 20f;
    public LayerMask interactLayer;

    public bool isEquipped = false;
    public bool isInspecting = false;
    public GameObject EquippedObject = null;
    public Transform ObjectEquipPoint;

    public Vector3 InspectingOriginalPosition;
    public Quaternion InspectingOriginalRotation;
    public Vector3 InspectingOriginalScale;

    public void Inspect(GameObject obj)
    {
        isInspecting = !isInspecting;
        Camera cam = controller.playerCamera;

        //Debug.Log("Original pos:"+inspectedItemOriginalState.position);
        //Debug.Log("Camera pos:"+cam.transform.position);

        if (isInspecting)
        {
            InspectingOriginalPosition = obj.transform.position; //Store original state
            InspectingOriginalRotation = obj.transform.rotation;

            controller.LockCamera();
            controller.playerMovement = false;
            Cursor.lockState = CursorLockMode.None;

            //Debug.Log("Original pos:" + InspectingOriginalPosition);
            obj.transform.position = cam.transform.position + cam.transform.forward * 2f; //Position After storing orignal state

        }
        else
        {


            //Debug.Log("Original pos after deinspecting:" + InspectingOriginalPosition);
            controller.UnlockCamera();
            controller.playerMovement = true;
            //Debug.Log("Back to original State pos:"+ InspectingOriginalPosition);
            obj.transform.position = InspectingOriginalPosition;
            //Debug.Log("After the pos was set:"+obj.transform.position);
            obj.transform.rotation = InspectingOriginalRotation;
            //obj.transform.localScale = InspectingOriginalScale;
            Cursor.lockState = CursorLockMode.Locked;
            InspectingOriginalPosition = Vector3.zero;
        }
    }
            public GameObject UnEquip() {
                isEquipped = false;

                GameObject tempEquipped = EquippedObject;
                tempEquipped.GetComponentInChildren<Collider>().isTrigger = false;

                EquippedObject = null;

                return tempEquipped;

            }

    public void Equip(GameObject obj) {
        if (!isEquipped)
        {
            EquippedObject = obj;

            isEquipped = true;

            obj.transform.SetParent(ObjectEquipPoint);
            obj.transform.position = ObjectEquipPoint.transform.position;
            //obj.transform.rotation = ObjectEquipPoint.transform.rotation;
            obj.transform.localScale = obj.transform.localScale / 2;
            obj.GetComponentInChildren<Collider>().isTrigger = true;


            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }
        }
    }

    public static void BridgeObjects() {
        foreach (Collider bridgeObject in PlayerCollisionScript.bridgeObjects)
        {
            bridgeObject.isTrigger = false;
            Debug.Log(bridgeObject.transform);
            bridgeObject.transform.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    public void Interact(InputAction.CallbackContext ctx)
    {
        if (!PlayerAbilityScript.isDrawing && ctx.performed && PlayerAbilityScript.abilities.Contains(Ability.canInteract))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if(Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
            {
                //Debug.Log("hit something");
                IPlayerInteractable interactable = hit.collider.GetComponentInParent<IPlayerInteractable>();

                if(interactable != null)
                {
                    //Debug.Log("Interacted with interactable");
                    interactable.Interact(this);
                }
            }

            //Debug.Log("Interaction Performed");
        }
    }

    public static void ScanObjects() {
        foreach (IScannable scannableObject in PlayerCollisionScript.scannableObjects)
        {
            scannableObject.Scan();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
