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
        /*        Vector3 baseDirection = playerCam.transform.forward;
                baseDirection.Normalize();
                float spreadX = Random.Range(-data.SpreadAngle, data.SpreadAngle);
                float spreadY = Random.Range(-data.SpreadAngle, data.SpreadAngle);
                Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 1);
                return spreadRotation * baseDirection;*/
        Vector3 baseDirection = playerCam.transform.forward.normalized;

        // Générer un offset aléatoire en angle
        float spreadAngleX = Random.Range(-data.SpreadAngle, data.SpreadAngle);
        float spreadAngleY = Random.Range(-data.SpreadAngle, data.SpreadAngle);

        // Créer une rotation autour des axes locaux (droite et haut par rapport à baseDirection)
        Quaternion rotationX = Quaternion.AngleAxis(spreadAngleX, playerCam.transform.right);
        Quaternion rotationY = Quaternion.AngleAxis(spreadAngleY, playerCam.transform.up);

        // Appliquer le spread au vecteur de base
        Vector3 spreadDirection = rotationY * rotationX * baseDirection;

        return spreadDirection.normalized;
    }
}
