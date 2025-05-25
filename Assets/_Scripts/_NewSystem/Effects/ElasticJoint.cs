using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElasticJoint : MonoBehaviour
{
    public Rigidbody rb1;           
    public Rigidbody rb2;           
    public Vector3 anchorPoint;     
    public float springConstant = 50f; 
    public float damping = 5f;      
    
    public LineRenderer lineRenderer;
    public List<GlueEffect> effects;
    

    // Initialisation lorsque les deux objets sont dynamiques.
    public void Initialize(Rigidbody dynamicRb, Rigidbody otherRb)
    {
        if (dynamicRb == null || otherRb == null) { return; }
        rb1 = dynamicRb;
        rb2 = otherRb;

        
        GlueEffect GE1 = rb1.gameObject.GetOrAddComponent<GlueEffect>();
        GlueEffect GE2 = rb2.gameObject.GetOrAddComponent<GlueEffect>();

        effects.Add(GE1);
        effects.Add(GE2);        

        lineRenderer.SetPosition(0, rb1.position);
        lineRenderer.SetPosition(1, rb2.position);
    }

    // Initialisation pour un lien entre un objet dynamique et un point statique.
    public void Initialize(Rigidbody dynamicRb, Vector3 anchor)
    {
        if(dynamicRb == null) { return; }
        rb1 = dynamicRb;
        rb2 = null;
        anchorPoint = anchor;

        GlueEffect GE1 = rb1.gameObject.GetOrAddComponent<GlueEffect>();       

        effects.Add(GE1);        

        lineRenderer.SetPosition(0, rb1.position);
        lineRenderer.SetPosition(1, anchorPoint);
    }

    void FixedUpdate()
    {
        if (rb1 == null)
            return;

        Vector3 delta = rb2 != null ? rb2.position - rb1.position : anchorPoint - rb1.position;

        float currentDistance = delta.magnitude * damping;
        
        Vector3 direction = delta.normalized;
        
        Vector3 force = springConstant * direction * currentDistance;

        rb1.AddForce(force);

        lineRenderer.SetPosition(0, rb1.position);
        lineRenderer.SetPosition(1, anchorPoint);

        if (rb2 != null)
        {
            rb2.AddForce(-force);
            lineRenderer.SetPosition(1, rb2.position);
        }
            
    }

    private void OnDestroy()
    {
        foreach(GlueEffect effect in effects)
        {
            Destroy(effect.gameObject);
        }
    }
}
