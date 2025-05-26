using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class E_Wave : MonoBehaviour
{
    public static E_Wave instance;

    private void Awake()
    {
        instance = this;
    }


    [Header("GeneralSettings")]
    [SerializeField]
    private float delayBetweenWave;
    [SerializeField]
    private int maxEnemyInMap;

    [Header("EnemySettings")]
    [SerializeField]
    private List<int> lifeIncrease;
    [SerializeField]
    private List<int> enemyNumberIncrease;
    [SerializeField]
    private int baseNumberOfEnemy;
    public int WaveIndex => _waveIndex;
    public bool isMaxEnemyInstanciate => _enemyInstantiateNumber >= maxEnemyInMap;
    public int TotalLifeIncrease => _totalLifeIncrease;
    public bool IsInWave => _isInWave;

    private int _waveIndex = 0;
    private bool _isInWave = false;
    private int _waveEnemyCount = 0;

    private int _additionalEnemyCount = 0;
    private int _totalLifeIncrease;

    private int _enemyInstantiateNumber = 0;
    private int _enemyNumberLeft = 0;

    private List<E_Spawner> spawnerInMap = new();

    void Start()
    {
        StartCoroutine(StartWaveinDelay());
    }

    IEnumerator StartWaveinDelay()
    {
        _isInWave = false;
        yield return new WaitForSeconds(delayBetweenWave);
        _isInWave = true;
        _waveEnemyCount = baseNumberOfEnemy + _additionalEnemyCount;
        _enemyNumberLeft = _waveEnemyCount;
    }

    public void Update()
    {
        if (!IsInWave)
            return;

        foreach (E_Spawner spawner in spawnerInMap)
        {
            if (_enemyInstantiateNumber >= maxEnemyInMap)
            {
                continue;
            }
            spawner.SpawnEnemy();
            OnSpawnEnemy();
        }
    }

    private void RoundEnd()
    {
        _additionalEnemyCount += enemyNumberIncrease[WaveIndex % enemyNumberIncrease.Count];
        _totalLifeIncrease += lifeIncrease[WaveIndex % lifeIncrease.Count];
        _waveIndex++;
        _enemyInstantiateNumber = 0;
        StartCoroutine(StartWaveinDelay());
    }

    public void OnSpawnEnemy()
    {
        _enemyInstantiateNumber++;
    }

    public void OnEnemyDie()
    {
        _enemyNumberLeft--;
        if (_enemyNumberLeft <= 0)
        {
            RoundEnd();
        }
    }
    
    public void AddSpawner(E_Spawner spawner) => spawnerInMap.Add(spawner);
}