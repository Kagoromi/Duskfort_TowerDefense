using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Waypoints")]
    [SerializeField] private Transform[] waypoints;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject fastEnemyPrefab;
    [SerializeField] private GameObject droneEnemyPrefab;
    [SerializeField] private GameObject cruiserEnemyPrefab;
    [SerializeField] private GameObject armoredEnemyPrefab;
    [SerializeField] private GameObject commanderEnemyPrefab;

    [Header("Wave Settings")]
    [SerializeField] private float timeBetweenEnemies = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWave = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    public Transform[] GetWaypoints()
    {
        return waypoints;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(2f);

        while (!GameManager.Instance.IsGameOver)
        {
            currentWave++;
            GameManager.Instance.NextWave();
            yield return StartCoroutine(SpawnWave(currentWave));
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private IEnumerator SpawnWave(int wave)
    {
        int enemyCount = 2 + wave * 3;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject prefab = GetEnemyPrefab(wave);
            Instantiate(prefab, waypoints[0].position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private GameObject GetEnemyPrefab(int wave)
    {
        int rand = Random.Range(0, 100);

        if (wave >= 7 && rand < 20) return armoredEnemyPrefab;
        if (wave >= 5 && rand < 30) return cruiserEnemyPrefab;
        if (wave >= 3 && rand < 40) return droneEnemyPrefab;
        return fastEnemyPrefab;
    }
}