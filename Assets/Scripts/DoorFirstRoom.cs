using NUnit.Framework;
using UnityEngine;

public class DoorFirstRoom : MonoBehaviour
{
    public PuzzleSlotScript[] PuzzleSlots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int matchingCount = 0;

    void Start()
    {
        foreach(PuzzleSlotScript slot in PuzzleSlots)
        {
            slot.OnMatchChanged += HandleMatchChanged;
        }
    }

    private void HandleMatchChanged(PuzzleSlotScript slot, bool MatchingCodes)
    {
        if (MatchingCodes)
        {
            matchingCount++;
        }
        else
        {
            matchingCount--;
        }

        if(matchingCount == 5)
        {
            OpenDoor();
        }
    }

    private void OpenDoor() {
        Debug.Log("Door Opened");

        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
