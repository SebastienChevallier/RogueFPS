using UnityEngine;
using UnityEngine.InputSystem;

public class P_Shoot : MonoBehaviour
{
    private P_Weapon _weaponManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _weaponManager = GetComponent<P_Weapon>();
    }

    public void Shoot()
    {
        _weaponManager.CurrentWeapon.Shoot();
    }

    public void OnInputShoot(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            Shoot();
        }
    }
}
