using UnityEngine;

public class NailGun : BaseWeapon
{
    public NailGunData data;
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f))
            {
                CreateBox();
            }
        }
    }

    private void CreateBox()
    {
        NailZone sphere = Instantiate(data.NailZonePrefab);
        
        sphere.transform.position = player.transform.position + Vector3.forward * 1;
        sphere.rb.AddForce(transform.forward * data.GunForce);
    }
}
