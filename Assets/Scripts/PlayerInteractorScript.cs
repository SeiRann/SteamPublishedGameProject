using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractorScript : MonoBehaviour
{
    private PlayerAbilityScript abilityScript;
    public Transform cameraTransform;

    public static float interactRange = 20f;
    public LayerMask interactLayer;

    public bool isEquipped = false;
    public GameObject EquippedObject = null;
    public Transform ObjectEquipPoint;


    public GameObject UnEquip() {
        isEquipped = false;

        GameObject tempEquipped = EquippedObject;

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
            obj.transform.rotation = ObjectEquipPoint.transform.rotation;
            obj.transform.localScale = obj.transform.localScale / 2;

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
        if (!PlayerAbilityScript.isDrawing && ctx.performed)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if(Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
            {
                Debug.Log("hit something");
                IPlayerInteractable interactable = hit.collider.GetComponentInParent<IPlayerInteractable>();

                if(interactable != null)
                {
                    Debug.Log("Interacted with interactable");
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
