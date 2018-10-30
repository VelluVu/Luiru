using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/Weapon")]

public class Weapons : ScriptableObject {

    string weaponName;
    public float weaponSpeed;
    public float shotSpeed;
    public int hitArea;

    public float GetWeaponSpeed()
    {
        return weaponSpeed;
    }

    public float GetShotSpeed()
    {
        return shotSpeed;
    }
    public int GetHitArea()
    {
        return hitArea;
    }

    internal string GetWeaponName()
    {
        return weaponName;
    }
}
