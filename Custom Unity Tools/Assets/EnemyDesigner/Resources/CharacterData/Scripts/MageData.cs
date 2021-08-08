using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;


[CreateAssetMenu(fileName = "Mage Data", menuName = "Character Data / Mage")]
public class MageData : CharacterData
{
    public MageDamageType damageType;
    public MageWeaponType weaponType;
}
