using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "Rogue Data", menuName = "Character Data / Rogue")]

public class RogueData : CharacterData
{
    public RogueWeaponType rogueWeaponType;
    public RogueStrategyType rogueStrategyType;
}
