using Unity.VisualScripting;
using UnityEngine;

public class ElectricSMG : BaseWeapon
{
    public float distance;
    public LayerMask layerMask;
    public int Damage;
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;
            SphereCastRep();
            ShootEvents.Invoke();
        }
    }

    private RaycastHit[] SphereCastRep()
    {
        RaycastHit[] hits = Physics.SphereCastAll(Camera.main.transform.position, 0.5f, Camera.main.transform.forward, distance, layerMask);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent<ElectricEffect>(out ElectricEffect EE))
            {
                EE.ActivateElectricEffect();
            }

            if (hit.collider.TryGetComponent<I_Health>(out I_Health health))
            {
                health.OnDecreaseHealth(Damage);
            }
        }
        return hits;
    }
}
