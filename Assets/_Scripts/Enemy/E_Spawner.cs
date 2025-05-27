using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawner : MonoBehaviour
{
    [SerializeField]
    private List<E_enemy> enemyPrefabs;

    private E_Wave waveManager;
    private bool canSpawn = false;

    private void Start()
    {
        waveManager = E_Wave.instance;
        waveManager.AddSpawner(this);
    }

    public void TrySpawnEnemy()
    {
        if (canSpawn)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            StartCoroutine(DelayOnSpawn());
        }
    }

    private IEnumerator DelayOnSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(waveManager.DelayOnSpawn);
        canSpawn = true;
    }
}
