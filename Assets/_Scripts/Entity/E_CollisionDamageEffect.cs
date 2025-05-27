using UnityEngine;

public class E_CollisionDamageEffect : MonoBehaviour
{
    public Rigidbody body;
    public E_Entity entity;
    public float damageMultiplier;
    public float maxDamage;
    public float minMagnitudeToDamage;

    public void OnCollisionEnter(Collision collision)
    {
        if (body.linearVelocity.magnitude >= minMagnitudeToDamage)
        {
            int damage = Mathf.RoundToInt(body.linearVelocity.magnitude - minMagnitudeToDamage * damageMultiplier);
            entity.OnDecreaseHealth(damage);
        }
    }
}
