using UnityEngine;

public class ElectricEffect : IEffect
{
    public void OnApply(PhysicObject obj)
    {
        obj.IsElectrified = true;
        obj.EnableElectricVisuals(true);
        Debug.Log("ElectricEffect appliqué");
    }

    public void OnRemove(PhysicObject obj)
    {
        obj.IsElectrified = false;
        obj.EnableElectricVisuals(false);
        Debug.Log("ElectricEffect retiré");
    }

    public void UpdateEffect(PhysicObject obj)
    {
        // ex : infliger dégâts périodiques si joueur est en contact
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