using System.Collections.Generic;
using UnityEngine;

public class E_Spawner : MonoBehaviour
{
    [SerializeField]
    private List<E_enemy> enemyPrefabs;

    private E_Wave waveManager;

    private void Start()
    {
        waveManager = E_Wave.instance;
        waveManager.AddSpawner(this);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
    }
}
