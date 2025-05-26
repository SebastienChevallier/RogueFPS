using UnityEngine;
using UnityEngine.Events;

public class BaseWeapon : AWeapon
{
    public Color weaponColor = Color.white;
    public HUD_InGame hud;
    public UnityEvent ShootEvents;
    public UnityEvent RecoilEvents;

    public void OnEnable()
    {
        hud.UpdateColorCursor(weaponColor);
    }

    public override void Recoil()
    {
        if(isRecoiling)
        {
            if (recoilTime < 10)
            {
                recoilTime += recoilSpeed;
                recoilTime = Mathf.Clamp(recoilTime, 0f, 10f);
                //Debug.Log("Recoiling..." + recoilTime*10 + "%");
            }
            else
            {                
                isRecoiling = false;
                recoilTime = 0;
                RecoilEvents.Invoke();
                //Debug.Log("Recoiled");
            }            
        }        
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void SecondaryShoot()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {

        if(!isRecoiling) 
        { 
            isRecoiling = true;
            //Debug.Log("Shoot");
            ShootEvents.Invoke();

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 100f))
            {
                //Debug.Log("Hit");
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Recoil();
    }
}
