using UnityEngine;

public class BaseWeapon : AWeapon
{
    public override void Recoil()
    {
        if(isRecoiling)
        {
            if (recoilTime < 10)
            {
                recoilTime += Time.time * recoilSpeed * 0.01f;
                recoilTime = Mathf.Clamp(recoilTime, 0f, 10f);
                Debug.Log("Recoiling..." + recoilTime*10 + "%");
            }
            else
            {                
                isRecoiling = false;
                recoilTime = 0;
                Debug.Log("Recoiled");
            }            
        }        
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        if(!isRecoiling) 
        { 
            isRecoiling = true;
            Debug.Log("Shoot");

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 100f))
            {
                Debug.Log("Hit");
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Recoil();
    }
}
