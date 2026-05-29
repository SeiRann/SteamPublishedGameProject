using UnityEngine;

public class PuzzlePiece : MonoBehaviour, IScannable, IPlayerInteractable
{
    public int PieceCode;
    public Texture SymbolImage;
    public GameObject Symbol;

    Renderer cubeRenderer;
    public bool puzzle = true;

    void Awake() { 
        cubeRenderer = GetComponentInChildren<Renderer>();
        Symbol.GetComponent<Renderer>().material.mainTexture = SymbolImage;
    }
    public void Scan() {
        cubeRenderer.material.color = Color.yellow;
        Debug.Log(GetScannableText());
    }

    public string GetScannableText() {
        return "Scanned a puzzle piece and it now illuminates";
    }

    public void Interact(PlayerInteractorScript player) {
        player.Equip(this.gameObject);
        
        //Debug.Log(GetInteractionText());
    }

    public string GetInteractionText() {
        return "Interacted with puzzle piece, it should be equipped in the player's hand";
    }
}

