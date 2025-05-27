using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Game/GeneralData/HealthData", order = 1)]
public class HealthData : ScriptableObject
{
    public int maxHealth;

    public int health;
}
