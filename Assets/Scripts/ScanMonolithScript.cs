using System.Collections.Generic;
using UnityEngine;

public class ScanMonolithScript : MonoBehaviour, IAbility, IPlayerInteractable 
{
    public void Interact(PlayerInteractorScript player) {
        UnlockAbility(PlayerAbilityScript.abilities);
    }

    void UnlockAbility(List<Ability> abilities)
    {
        abilities.Add(Ability.canScan);
        abilities.Add(Ability.canDraw);
    }

    public string GetInteractionText() {
        return "";
    }

    public string GetAbilityText() {
        return "Unlocked the ability to scan and draw";
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
