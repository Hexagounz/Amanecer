using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDoorWaveController : MonoBehaviour
{
    [SerializeField] private GameObject _fungusPrefab;

    [SerializeField] private GameObject _flowerPrefab;

    private int _currentWave;

    [SerializeField] private List<GameObject> _fungusSpawnPoints;

    [SerializeField] private List<GameObject> _flowersSpawnPoints;

    private List<GameObject> _currentEnemys;
    
    [SerializeField] private GameObject _spawnParticles;

    [SerializeField] private GameObject _blocketDoor;

    [SerializeField] private GameObject doorPoint;

    [SerializeField] private GameObject healTutorialTrigger;
    private void Start()
    {
        _currentEnemys = new List<GameObject>();
        WaveSpawner(_currentWave);
    }

    private void WaveSpawner(int index)
    {
        switch (index)
        {
            // 1 hongo
            case 0: SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[0]);
                break;
            // 3 hongos
            case 1:
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[0]);
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[1]);
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[2]);
                break;
            // 2 hongos 1 flor
            case 2:
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[0]);
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[2]);
                SpawnEnemy(_flowerPrefab, _flowersSpawnPoints[0]);
                break;
            // 2 flores
            case 3:
                SpawnEnemy(_flowerPrefab, _flowersSpawnPoints[0]);
                SpawnEnemy(_flowerPrefab, _flowersSpawnPoints[1]);
                break;
            // 3 hongos 2 flores
            case 4:
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[0]);
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[1]);
                SpawnEnemy(_fungusPrefab, _fungusSpawnPoints[2]);
                SpawnEnemy(_flowerPrefab, _flowersSpawnPoints[0]);
                SpawnEnemy(_flowerPrefab, _flowersSpawnPoints[1]);
                break;
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, GameObject spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        GameObject particles = Instantiate(_spawnParticles, spawnPoint.transform.position, Quaternion.identity);
        _currentEnemys.Add(enemy);
        enemy.AddComponent<WaveEnemy>().OnDeath += WaveChecker;
        Destroy(particles, 2f);
    }
    

    private void WaveChecker(WaveEnemy enemy)
    {
        enemy.OnDeath -= WaveChecker;
        if(_currentEnemys.Contains(enemy.gameObject))
            _currentEnemys.Remove(enemy.gameObject);



        if (_currentEnemys.Count == 0 && _currentWave < 4)
        {
            _currentWave++;
            WaveSpawner(_currentWave);
        }else if (_currentEnemys.Count == 0 && _currentWave == 4)
        {
            CompletedWaves();
        }
        

    }

    private void CompletedWaves()
    {
        _blocketDoor.SetActive(false);
        GameObject particles = Instantiate(_spawnParticles, doorPoint.transform.position, Quaternion.identity);
        healTutorialTrigger.SetActive(true);
        Destroy(particles, 2f);
    }

}
