using UnityEngine;
using System;

public class PuzzleSlotScript : MonoBehaviour, IPlayerInteractable, IScannable
{
    public Transform puzzleSlotPoint;
    public GameObject puzzlePiece;
    public int SlotCode;
    public bool MatchingCodes = false;

    public void Interact(PlayerInteractorScript player)
    {
        if (player.isEquipped && player.EquippedObject.GetComponentInParent<PuzzlePiece>())
        {
            puzzlePiece = player.UnEquip();
            Debug.Log("unequipped");
            puzzlePiece.transform.SetParent(puzzleSlotPoint);
            puzzlePiece.transform.position = puzzleSlotPoint.transform.position;
            puzzlePiece.transform.rotation = Quaternion.identity;
            puzzlePiece.transform.localScale = puzzlePiece.transform.localScale * 2;
            checkCodes(puzzlePiece.GetComponentInParent<PuzzlePiece>());
        }
        Debug.Log(puzzleSlotPoint);
    }


    public void checkCodes(PuzzlePiece obj)
    {
        if (obj.PieceCode == SlotCode)
        {
            Debug.Log("Is matching");
            MatchingCodes = true;
        }else
        {
            Debug.Log("Is not Matching");
            MatchingCodes = false;
        }

        OnMatchChanged?.Invoke(this,MatchingCodes);
    }

    public event Action<PuzzleSlotScript, bool> OnMatchChanged;

    public string GetInteractionText() {
        return "Puzzle piece from player is now equipped on the puzzle slot";
    }

    public void Scan() { }

    public string GetScannableText() {
        return "If puzzle was slotted it would be placed in the slot rn";
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
