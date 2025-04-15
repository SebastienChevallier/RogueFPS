using UnityEngine;

public class NailGun : BaseWeapon
{
    public NailGunData data;
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;

            CreateBox();
        }
    }

    private void CreateBox()
    {
        NailZone sphere = Instantiate(data.NailZonePrefab);
        sphere.Init(data);
        sphere.gameObject.transform.position = transform.position + transform.forward * 1.5f;
        sphere.rb.AddForce(transform.forward * data.GunForce);
    }
}
