using UnityEngine;

public class RepulsorEffect : AEffect
{
    public Rigidbody body;
    public override void OnEffectHit(object[] arg)
    {        
        body.AddForce((Vector3)arg[0], ForceMode.Impulse);
    }

    public override void OnHit(object[] arg)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
