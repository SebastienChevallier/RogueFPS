using UnityEngine;

public class Repulsor : BaseWeapon
{
    public float force;
    public float distance;
    
    public override void Shoot()
    {
        if (!isRecoiling)
        {
            isRecoiling = true;            

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, distance))
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    if(player.TryGetComponent<P_Movement>(out P_Movement comp))
                    {
                        //comp._velocity.y = 0;
                        //comp._rb.AddForce(-Camera.main.transform.forward * force * 5, ForceMode.Force);
                        comp.Impulse(-transform.forward * force);
                    }                    
                }

                if (hit.collider.TryGetComponent<RepulsorEffect>(out RepulsorEffect RE))
                {
                    Vector3 direction = -(transform.position - hit.transform.position);
                    object[] arg = new object[] { (direction * force) };
                    RE.OnEffectHit(arg);
                }
            }
        }
    }
}
