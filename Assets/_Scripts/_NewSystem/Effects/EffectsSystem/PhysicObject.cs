using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    private List<IEffect> activeEffects = new List<IEffect>();

    public bool IsSticky { get; set; }
    public bool IsElectrified { get; set; }

    public void AddEffect(IEffect effect)
    {
        if (!activeEffects.Any(e => e.GetType() == effect.GetType()))
        {
            activeEffects.Add(effect);
            effect.OnApply(this);
        }
    }

    public void RemoveEffect<T>() where T : IEffect
    {
        var effect = activeEffects.FirstOrDefault(e => e is T);
        if (effect != null)
        {
            effect.OnRemove(this);
            activeEffects.Remove(effect);
        }
    }

    public bool HasEffect<T>() where T : IEffect
    {
        return activeEffects.Any(e => e is T);
    }

    public void Update()
    {
        foreach (var effect in activeEffects)
        {
            effect.UpdateEffect(this);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach (var effect in activeEffects)
        {
            effect.OnCollisionEnter(this, collision);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        foreach (var effect in activeEffects)
        {
            effect.OnCollisionExit(this, collision);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        foreach (var effect in activeEffects)
        {
            effect.OnTriggerEnter(this, other);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        foreach (var effect in activeEffects)
        {
            effect.OnTriggerExit(this, other);
        }
    }

    public void OnDestroy()
    {
        foreach (var effect in activeEffects)
        {
            effect.OnDestroy(this);
        }
    }

    public void ModifyDrag(float value)
    {
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearDamping = value;
        }
    }

    public void EnableElectricVisuals(bool state)
    {
        // ex: activer/desactiver des particules ou un shader
    }
}
