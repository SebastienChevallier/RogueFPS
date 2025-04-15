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
        sphere.rb.AddForce(direction * data.GunForce);
    }

    Vector3 GetSpreadDirection()
    {
        //Vector3 baseDirection = playerCam.transform.forward;
        Vector3 baseDirection = (playerCam.transform.forward + player.transform.forward);
        baseDirection.Normalize(); // très important
        float spreadX = Random.Range(-data.SpreadAngle, data.SpreadAngle);
        float spreadY = Random.Range(-data.SpreadAngle, data.SpreadAngle);
        Quaternion spreadRotation = Quaternion.Euler(spreadY, spreadX, 0);
        return spreadRotation * baseDirection;
    }
}
