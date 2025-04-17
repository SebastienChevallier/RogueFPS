using UnityEngine;

public class NailGun : BaseWeapon
{
    public NailGunData data;
    public Transform playerCam;
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;
            for (int i = 0; i < bulletsPerShoot; i++)
            {
                InstantiateNail();
            }
        }
    }

    private void InstantiateNail()
    {
        Vector3 direction = GetSpreadDirection();
        NailZone sphere = Instantiate(data.NailZonePrefab);
        sphere.Init(data);
        sphere.gameObject.transform.position = transform.position + transform.forward * 1.5f;
        sphere.gameObject.transform.rotation = Quaternion.identity;
        sphere.gameObject.transform.rotation = Quaternion.LookRotation(direction);

        sphere.rb.AddForce(direction * data.GunForce);
    }

    Vector3 GetSpreadDirection()
    {
        Vector3 baseDirection = playerCam.transform.forward.normalized;

        float spreadAngleX = Random.Range(-data.SpreadAngle, data.SpreadAngle);
        float spreadAngleY = Random.Range(-data.SpreadAngle, data.SpreadAngle);

        Quaternion rotationX = Quaternion.AngleAxis(spreadAngleX, playerCam.transform.right);
        Quaternion rotationY = Quaternion.AngleAxis(spreadAngleY, playerCam.transform.up);
    
        Vector3 spreadDirection = rotationY * rotationX * baseDirection;

        return spreadDirection.normalized;
    }
}
