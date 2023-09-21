using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityWaveController : MonoBehaviour
{
    [SerializeField] private GameObject _soldierPrefab;

    [SerializeField] private GameObject _tankPrefab;

    private int _currentWave;

    [SerializeField] private List<GameObject> _soldierSpawnPoints;

    [SerializeField] private List<GameObject> _tankSpawnPoints;
    
    private List<GameObject> _currentEnemys;
    
    [SerializeField] private GameObject _spawnParticles;

    [SerializeField] private GameObject _blocketDoor;

    [SerializeField] private GameObject doorPoint;

    [SerializeField] private Rigidbody _blockRockRigidBody;

    [SerializeField] private GameObject _blockCollider;
    // Start is called before the first frame update
    void Start()
    {
        _currentEnemys = new List<GameObject>();
    }

    public void StartWaves()
    {
        _blockCollider.SetActive(true);
        _blockRockRigidBody.useGravity = true;
        WaveSpawner(_currentWave);
    }
    
    
    private void WaveSpawner(int index)
    {
        switch (index)
        {
            // 2 soldados
            case 0: SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[0]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[1]);
                break;
            // 5 soldados
            case 1:
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[0]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[1]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[2]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[3]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[4]);
                break;
            // 3 soldados 1 tanque
            case 2:
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[2]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[3]);
                SpawnEnemy(_soldierPrefab, _soldierSpawnPoints[4]);
                SpawnEnemy(_tankPrefab, _tankSpawnPoints[0]);
                break;
            // 2 tanques
            case 3:
                SpawnEnemy(_tankPrefab, _tankSpawnPoints[0]);
                SpawnEnemy(_tankPrefab, _tankSpawnPoints[1]);
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



        if (_currentEnemys.Count == 0 && _currentWave < 3)
        {
            _currentWave++;
            WaveSpawner(_currentWave);
        }else if (_currentEnemys.Count == 0 && _currentWave == 3)
        {
            CompletedWaves();
        }
        

    }
    
    
    private void CompletedWaves()
    {
        _blocketDoor.SetActive(false);
        GameObject particles = Instantiate(_spawnParticles, doorPoint.transform.position, Quaternion.identity);
        Destroy(particles, 2f);
    }

    
}
