using System.Collections.Generic;
using UnityEngine;


public class GlueEffect : AEffect
{
    [SerializeField]
    public LayerMask LayerMask;
    public List<GameObject> childs;

    public override void OnEffectHit(object[] arg)
    {
        
    }

    public override void OnHit(object[] arg)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LayerMask = 1 << 7;
    }

    private void OnEnable()
    {
        LayerMask = 1 << 7;
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask = 1 << 7;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if ((LayerMask.value & (1 << other.gameObject.layer)) != 0)
        {            
            if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                other.gameObject.transform.SetParent(transform, true);
                
                rb.isKinematic = true;
                DesActiveColliders(other.gameObject);
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
        childs.Add(gameObject);
    }

    private void OnDestroy()
    {
        foreach (GameObject child in childs)
        {
            Destroy(child);
        }
    }
}
