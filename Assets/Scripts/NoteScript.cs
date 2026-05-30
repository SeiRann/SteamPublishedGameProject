using UnityEngine;

public class NoteScript : MonoBehaviour, IPlayerInteractable
{
    public void Interact(PlayerInteractorScript player) {
        player.Inspect(this.gameObject);
    }

    public string GetInteractionText() {

        return "Interacted with Note";
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
