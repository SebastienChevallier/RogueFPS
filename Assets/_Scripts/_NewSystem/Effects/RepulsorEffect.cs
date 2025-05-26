using System.Collections;
using UnityEngine;

public class RepulsorEffect : AEffect
{
    public Rigidbody body;
    public GameObject visual;
    public AnimationCurve curve;
    public float animationTime;
    private float timer = 100f;

    public override void OnEffectHit(object[] arg)
    {        
        if(body != null)
        {
            body.AddForce((Vector3)arg[0], ForceMode.Impulse);
        }
    }

    public override void OnHit(object[] arg)
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (body != null && body.linearVelocity.magnitude > 5f)
        {
            timer = 0f;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(timer <= animationTime)
        {
            timer += Time.deltaTime;           
            visual.transform.localScale = curve.Evaluate(timer / animationTime) * Vector3.one;
        }
    }
}
