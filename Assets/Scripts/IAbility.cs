using UnityEngine;
using System.Collections.Generic;

public interface IAbility
{
    void UnlockAbility(List<Ability> playerAbilities) {}
    string GetAbilityText();
}