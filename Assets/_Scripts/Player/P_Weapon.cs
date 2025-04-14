using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class P_Weapon : MonoBehaviour
{
    public List<AWeapon> Weapons;
    public AWeapon CurrentWeapon;

    public void Awake()
    {
        Weapons.Clear();
        AWeapon[] w = GetComponentsInChildren<AWeapon>();

        if(w.Length > 0)
        {
            foreach (AWeapon weapon in w)
            {
                Weapons.Add(weapon);
                weapon.gameObject.SetActive(false);
            }

            CurrentWeapon = Weapons[0];
            CurrentWeapon.gameObject.SetActive(true);
        }        
    }
}
