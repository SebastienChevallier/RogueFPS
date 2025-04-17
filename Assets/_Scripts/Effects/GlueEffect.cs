using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GlueEffect : AEffect
{
    [SerializeField]
    public LayerMask LayerMask;
    public override void OnEffectHit(object[] arg)
    {
        
    }

    public override void OnHit(object[] arg)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((LayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {            
            if(collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                collision.gameObject.transform.SetParent(transform, true);
                

                rb.isKinematic = true;                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.value & (1 << other.gameObject.layer)) != 0)
        {            
            if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                other.gameObject.transform.SetParent(transform, true);
                DesActiveColliders(other.gameObject);
                rb.isKinematic = true;
            }
        }
    }

    public void DesActiveColliders(GameObject gameObject)
    {
        BoxCollider[] colliders = gameObject.GetComponentsInChildren<BoxCollider>();

        foreach (BoxCollider collider in colliders)
        {
            collider.isTrigger = true;
        }
    }
}
