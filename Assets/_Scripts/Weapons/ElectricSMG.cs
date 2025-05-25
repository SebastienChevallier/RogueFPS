using Unity.VisualScripting;
using UnityEngine;

public class ElectricSMG : BaseWeapon
{
    public float distance;
    public LayerMask layerMask;
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;
            SphereCastRep();
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
        }
        return hits;
    }
}
