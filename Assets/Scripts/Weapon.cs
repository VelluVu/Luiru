using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Weapons[] weps;
    public float fireSpeed;
    public float ammoSpeed;
    public string weaponN;
    public int hitA;
    
    public Weapon GetWeapon(int index)
    {

        for (int i = 0; i < weps.Length; i++)
        {
            if (i == index)
            {
                Debug.Log("Getting Weapon: " + weps[i] + " " + fireSpeed  + " " + ammoSpeed);
                fireSpeed = weps[i].GetWeaponSpeed();
                ammoSpeed = weps[i].GetShotSpeed();
                weaponN = weps[i].GetWeaponName();
                hitA = weps[i].GetHitArea();

                return this;
            }
        }
        return null;
    }

}
