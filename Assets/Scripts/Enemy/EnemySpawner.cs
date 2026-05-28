using UnityEngine;

/// <summary>
/// 게임 진행 중 일정 시간마다 적 프리팹을 생성한다.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float spawnInterval = 1.5f;

    [SerializeField] private float spawnDistance = 8.0f;

    [SerializeField] private int maxEnemyCount = 20;

    private float spawnTimer = 0.0f;
    
    // Update is called once per frame
    void Update()
    {
        if(enemyPrefab == null || playerTransform == null)
        {
            return;
        }

        //======================시간 누적======================
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0.0f;
            // 적 스폰 처리.
            TrySpawnEnemy();
        }
        //====================================================

        //======================시간 차감======================
        //spawnTimer -= Time.deltaTime;
        //if (spawnTimer <= 0.0f)
        //{
        //    spawnTimer = spawnInterval;
        //    TrySpawnEnemy();
        //}
        //====================================================
    }

    void TrySpawnEnemy()
    {
        if(enemyParent.childCount >= maxEnemyCount)
        {
            return;
        }

        // 스폰 위치 계산.
        Vector3 spawnPosition = GetSpawnPositionAroundPlayer();

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent);
    }

    Vector3 GetSpawnPositionAroundPlayer()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        if(randomDirection == Vector2.zero)
        {
            randomDirection = Vector2.right;
        }

        Vector3 spawnOffset = new Vector3(randomDirection.x, randomDirection.y, 0.0f);

        spawnOffset *= spawnDistance;

        return playerTransform.position + spawnOffset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, spawnDistance);
    }
}
