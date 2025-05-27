using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawner : MonoBehaviour
{
    [SerializeField]
    private List<E_enemy> enemyPrefabs;

    private E_Wave waveManager;
    private bool canSpawn = true;

    private void Start()
    {
        waveManager = E_Wave.instance;
        waveManager.AddSpawner(this);
    }

    public bool TrySpawnEnemy()
    {
        if (canSpawn)
        {
            E_enemy go = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            go.transform.position = transform.position;
            StartCoroutine(DelayOnSpawn());
            return true;
        }
        return false;
    }

    private IEnumerator DelayOnSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(waveManager.DelayOnSpawn);
        canSpawn = true;
    }
}
