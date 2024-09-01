using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int strength,
        defense;
    public int level,
        currentExp,
        expToLevel;
    public int maxHP,
        currentHP;
    public string currentLevel;

    //store a reference for the weapon type enum from within the DungeonWeaponController script    public DungeonWeaponController.weaponType currentWeapon;
}
