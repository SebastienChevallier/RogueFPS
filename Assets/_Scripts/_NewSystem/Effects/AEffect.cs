using UnityEngine;

public abstract class AEffect : MonoBehaviour
{
    public abstract void OnHit(object[] arg);
    public abstract void OnEffectHit(object[] arg);
}

public enum Effects
{

}
