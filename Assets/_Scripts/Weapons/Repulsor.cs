using UnityEngine;

public class Repulsor : BaseWeapon
{
    public float force;
    public float jumpForce;
    public float distance;
    
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;
            ShootEvents.Invoke();
            SphereCastRep();

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, distance/2))
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    if(player.TryGetComponent<P_Movement>(out P_Movement comp))
                    {
                        comp.Impulse(-transform.forward * jumpForce);
                    }                    
                }
            }
        }
    }

    private RaycastHit[] SphereCastRep()
    {
        RaycastHit[] hits = Physics.SphereCastAll(Camera.main.transform.position, 0.5f, Camera.main.transform.forward, distance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent<RepulsorEffect>(out RepulsorEffect RE))
            {
                Vector3 direction = -(transform.position - hit.transform.position);
                object[] arg = new object[] { (direction * force) };
                RE.OnEffectHit(arg);
            }
        }
        return hits;
    }
}
