using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "Warrior Data", menuName = "Character Data / Warrior")]

public class WarriorData : CharacterData
{
    public WarriorClassType warriorClassType;
    public WarriorWeaponType warriorWeaponType;
}
