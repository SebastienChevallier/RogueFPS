using UnityEngine;

public class StickyEffect : IEffect
{
    public void OnApply(PhysicObject obj)
    {
        obj.IsSticky = true;
        obj.ModifyDrag(5f);
        Debug.Log("StickyEffect appliqué");
    }

    public void OnRemove(PhysicObject obj)
    {
        obj.IsSticky = false;
        obj.ModifyDrag(1f);
        Debug.Log("StickyEffect retiré");
    }

    public void UpdateEffect(PhysicObject obj)
    {
        // ex : garder la friction élevée
    }

    void IEffect.OnCollisionEnter(PhysicObject obj, Collision collision)
    {

    }

    void IEffect.OnCollisionExit(PhysicObject obj, Collision collision)
    {
    }

    void IEffect.OnDestroy(PhysicObject obj)
    {
    }

    void IEffect.OnTriggerEnter(PhysicObject obj, Collider other)
    {
    }

    void IEffect.OnTriggerExit(PhysicObject obj, Collider other)
    {
    }
}
