using System.Collections.Generic;
using UnityEngine;

public class NailZone : MonoBehaviour
{
    public Rigidbody rb;
    private string layerName;

    private Dictionary<E_enemy, float> enemyTimers = new Dictionary<E_enemy, float>();
    private Dictionary<E_enemy, float> enemyTimersSecure = new Dictionary<E_enemy, float>();

    private NailGunData _nailData;
    public void Init(NailGunData nailData)
    {
        layerName = "Enemy";
        _nailData = nailData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            print("add");
            enemyTimers.Add(other.GetComponent<E_enemy>(), 0);
        }
    }
    void Update()
    {
        // Liste des ennemis à retirer à la fin
        List<E_enemy> toRemove = new List<E_enemy>();

        // On fait une copie des paires pour itérer en sécurité
        var enemies = new List<KeyValuePair<E_enemy, float>>(enemyTimers);

        foreach (var pair in enemies)
        {
            E_enemy enemy = pair.Key;

            if (enemy == null)
            {
                toRemove.Add(enemy);
                continue;
            }

            float newTimer = enemyTimers[enemy] - Time.deltaTime;

            if (newTimer <= 0f)
            {
                Damage(enemy);
                enemyTimers[enemy] = _nailData.Delay; // reset timer
            }
            else
            {
                enemyTimers[enemy] = newTimer;
            }
        }

        foreach (E_enemy enemy in toRemove)
        {
            enemyTimers.Remove(enemy);
        }
    }

    private void UpdateList()
    {
        enemyTimersSecure.Clear();
        foreach (var pair in enemyTimers)
        {
            enemyTimersSecure.Add(pair.Key, enemyTimers[pair.Key]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            print("Remove");
            enemyTimers.Remove(other.GetComponent<E_enemy>());
        }
    }

    private void Damage(E_Entity entity)
    {
        entity.OnDecreaseHealth(_nailData.Damage);
        print($"Damage : {_nailData.Damage}, to {entity.name}");
    }
}
