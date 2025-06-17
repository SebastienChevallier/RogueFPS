using UnityEngine;
using UnityEngine.Events;

public class Bumper : MonoBehaviour
{
    public float power;
    public UnityEvent eventTrigger;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<RepulsorEffect>(out RepulsorEffect re))
        {
            //Vector3 direction = power * re.body.linearVelocity.magnitude * -(transform.position - collision.transform.position) / 2;
            Vector3 direction =  Vector3.Reflect(re.body.linearVelocity, collision.GetContact(0).normal);
            direction.y = 0;

            object[] objects = new object[]
            {
                 direction * power
            };
            //Debug.Log(objects[0]);
            re.OnEffectHit(objects);
            eventTrigger.Invoke();
        }
    }
}
