using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class P_Weapon : MonoBehaviour
{
    public List<AWeapon> Weapons;
    public AWeapon CurrentWeapon;
    public int indexWeapon = 0;

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

            CurrentWeapon = Weapons[indexWeapon];
            CurrentWeapon.gameObject.SetActive(true);
        }        
    }

    public void OnInputScroll(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            Vector2 input = context.ReadValue<Vector2>();            
            ChangeWeaponIndex((int)input.y);            
        }
    }

    private void ChangeWeaponIndex(int val)
    {
        if(indexWeapon < Weapons.Count)
        {
            indexWeapon += val;

            if (indexWeapon <= 0) 
            { 
                indexWeapon = Weapons.Count-1;
                
            }
            else if(indexWeapon >= Weapons.Count)
            {
                indexWeapon = 0;
                
            }
        }   

        
        Debug.Log(indexWeapon);
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = Weapons[indexWeapon];
        CurrentWeapon.gameObject.SetActive(true);
    }
}
