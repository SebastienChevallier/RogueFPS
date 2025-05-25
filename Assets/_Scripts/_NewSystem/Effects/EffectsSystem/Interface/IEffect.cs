using UnityEngine;

public interface IEffect
{
    void OnApply(PhysicObject obj);
    void OnRemove(PhysicObject obj);
    void UpdateEffect(PhysicObject obj);

    void OnCollisionEnter(PhysicObject obj, Collision collision);

    void OnCollisionExit(PhysicObject obj, Collision collision);
    void OnTriggerEnter(PhysicObject obj, Collider other);
    void OnTriggerExit(PhysicObject obj, Collider other);

    void OnDestroy(PhysicObject obj);
}