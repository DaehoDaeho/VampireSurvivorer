using UnityEngine;

/// <summary>
/// 게임 진행 중 일정 시간마다 적 프리팹을 생성한다.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyParent;
    
    [SerializeField] private float spawnDistance = 8.0f;

    [SerializeField] private float baseSpawnInterval = 2.0f;
    [SerializeField] private float minSpawnInterval = 0.45f;
    [SerializeField] private float diffisultyDuration = 180.0f;

    [SerializeField] private int baseSpawnCount = 1;
    [SerializeField] private int maxSpawnCount = 6;

    [SerializeField] private float spawnCountIncreaseInterval = 30.0f;

    private float elapsedTime = 0.0f;
    private float spawnTimer = 0.0f;
    [SerializeField] private float currentSpawnInterval = 2.0f;
    [SerializeField] private int currentSpawnCount = 1;
    [SerializeField] private int currentWaveNumber = 0;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        UpdateDifficultyByTime();
        currentWaveNumber = GetCurrentWaveNumber();

        spawnTimer += Time.deltaTime;
        if(spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0.0f;
            SpawnEnemyGroup(currentSpawnCount);
        }
    }

    void UpdateDifficultyByTime()
    {
        // Mathf.Clamp01 : 0~1 사이의 범위를 벗어나지 않도록 보정해 주는 함수.
        float difficultyRatio = Mathf.Clamp01(elapsedTime / diffisultyDuration);

        // Mathf.Lerp : 시작 값에서 목표 값까지 부드럽게 변화시켜주는 함수.
        currentSpawnInterval = Mathf.Lerp(baseSpawnInterval, minSpawnInterval, difficultyRatio);

        // Mathf.FloorToInt : 내림 처리를 한 결과를 int 형 값으로 반환하는 함수.
        int addedCount = Mathf.FloorToInt(elapsedTime / spawnCountIncreaseInterval);

        // Mathf.Clamp : 첫번째 인자 값이 두번째 인자와 세번째 인자 사이 범위를 벗어나지 않도록 보정시켜주는 함수.
        currentSpawnCount = Mathf.Clamp(baseSpawnCount + addedCount, baseSpawnCount, maxSpawnCount);
    }

    void SpawnEnemyGroup(int spawnCount)
    {
        for(int i=0; i<spawnCount; ++i)
        {
            SpawnOneEnemy();
        }
    }

    void SpawnOneEnemy()
    {
        GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = playerTransform.position + (Vector3)(randomDirection * spawnDistance);

        Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity, enemyParent);
    }

    int GetCurrentWaveNumber()
    {
        int waveNumber = Mathf.FloorToInt(elapsedTime / spawnCountIncreaseInterval) + 1;

        return waveNumber;
    }

    GameObject GetRandomEnemyPrefab()
    {
        if(enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            return enemyPrefab;
        }

        // int일 경우 : 첫번째 인자가 그대로 최소 범위.
        //             두번째 인자 - 1이 최대 범위.
        // float일 경우 : 첫번째 인자가 그대로 최소 범위.
        //               두번째 인자가 그대로 최대 범위.
        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        return enemyPrefabs[randomIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, spawnDistance);
    }
}
