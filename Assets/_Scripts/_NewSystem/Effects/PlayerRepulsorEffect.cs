using UnityEngine;

public class PlayerRepulsorEffect : RepulsorEffect
{
    public P_Movement playerMovement;
    [Range(0.01f, 1f)]
    public float power = 0.1f;
    public override void OnEffectHit(object[] arg)
    {
        if(playerMovement != null)
        {
            playerMovement.Impulse((Vector3)arg[0] * power);
        }
    }
}
