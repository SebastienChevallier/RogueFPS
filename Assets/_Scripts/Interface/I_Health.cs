using UnityEngine;

public interface I_Health
{
    public void OnIncreaseHealth(int amount);

    public void OnDecreaseHealth(int amount);

    public void OnDie();

}
