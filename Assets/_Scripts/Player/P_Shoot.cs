using UnityEngine;
using UnityEngine.InputSystem;

public class P_Shoot : MonoBehaviour
{
    private P_Weapon _weaponManager;
    private bool _isShooting;


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
            //Shoot();
            _isShooting = true;
        }  
        
        if(context.canceled)
        {
            _isShooting = false;
        }
    }

    private void Update()
    {
        if(_isShooting)
        {
            Shoot();
        }
    }
}
