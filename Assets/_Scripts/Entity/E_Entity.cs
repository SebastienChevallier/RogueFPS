using UnityEngine;

public class E_Entity : MonoBehaviour, I_Health
{
    public HealthData healthData;
    public virtual void Start()
    {
        healthData = Instantiate(healthData);
        healthData.health = healthData.maxHealth;
    }

    public void OnIncreaseHealth(int amount)
    {
        healthData.health = Mathf.Clamp(healthData.health += amount,0,healthData.maxHealth);
    }

    public void OnDecreaseHealth(int amount)
    {
        healthData.health -= amount;
        if (healthData.health <= 0)
        {
            OnDie();
        }
    }

    public void OnDie()
    {
       gameObject.SetActive(false);
    }
}
