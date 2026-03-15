using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject[] enemyPrefabs;  // drag different enemy types here
        public int baseEnemyCount;
        public float timeBetweenSpawns = 0.5f;
    }

    [SerializeField] Wave[] m_waves;
    [SerializeField] Transform[] m_spawnPoints;
    [SerializeField] float m_timeBetweenWaves = 5f;
    [SerializeField] float m_enemyCountMultiplier = 1.5f; // exponential growth

    private int m_currentWave = 0;
    private List<GameObject> m_aliveEnemies = new List<GameObject>();
    private bool m_spawning = false;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (m_spawning) return;

        // Clean up destroyed enemies from list
        m_aliveEnemies.RemoveAll(e => e == null);

        // Start next wave when all enemies are dead
        if (m_aliveEnemies.Count == 0 && m_currentWave < m_waves.Length)
        {
            StartCoroutine(StartWave());
        }
    }

    IEnumerator StartWave()
    {
        m_spawning = true;

        yield return new WaitForSeconds(m_timeBetweenWaves);

        Wave wave = m_waves[m_currentWave];

        // Exponential enemy count — each wave has more enemies
        int enemyCount = Mathf.RoundToInt(
            wave.baseEnemyCount * Mathf.Pow(m_enemyCountMultiplier, m_currentWave)
        );

        Debug.Log($"Wave {m_currentWave + 1} starting — spawning {enemyCount} enemies");

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.timeBetweenSpawns);
        }

        m_currentWave++;
        m_spawning = false;
    }

    void SpawnEnemy(Wave wave)
    {
        // Pick random enemy type from wave
        GameObject prefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        // Pick random spawn point
        Transform spawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];

        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        m_aliveEnemies.Add(enemy);
    }
}