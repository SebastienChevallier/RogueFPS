using System.Collections.Generic;
using UnityEngine;


public class GlueEffect : AEffect
{
    [SerializeField]
    public LayerMask LayerMask;
    public List<GameObject> childs = new List<GameObject>();
    public int maxChilds = 50;

    public GameObject FX;

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
        if(transform.childCount >= 1)
        {
            FX = transform.GetChild(0).GetChild(0).gameObject;
            FX.SetActive(true);
        }
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
        if ((LayerMask.value & (1 << other.gameObject.layer)) != 0 && childs.Count <= maxChilds)
        {
            if (!other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                return;
            }
            other.gameObject.transform.SetParent(transform, true);

            rb.isKinematic = true;
            DesActiveColliders(other.gameObject);

            Destroy(rb);
        }
    }

    public void DesActiveColliders(GameObject gameObject)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.isTrigger = true;
        }

        if(childs.Contains(gameObject))
            return;

        childs.Add(gameObject);
        maxChilds++;
    }

    private void OnDestroy()
    {
        if (FX != null)
        {
            FX.SetActive(false);
        }

        if (childs == null || childs.Count == 0)
            return;

        foreach (GameObject child in childs)
        {
            Destroy(child);
        }
    }
}
