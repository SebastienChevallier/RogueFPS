using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float power;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<RepulsorEffect>(out RepulsorEffect re))
        {
            object[] objects = new object[] { power * re.body.linearVelocity.magnitude * -(transform.position - collision.transform.position)/2 };
            //Debug.Log(objects[0]);
            re.OnEffectHit(objects);
        }
    }
}
