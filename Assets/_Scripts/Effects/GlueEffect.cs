using UnityEngine;

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
        if(collision.gameObject.layer == LayerMask)
        {
            collision.gameObject.transform.SetParent(transform, false);
        }
    }
}
