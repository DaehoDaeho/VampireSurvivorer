using UnityEngine;

/// <summary>
/// Player 주변의 적을 탐색하고, 가장 가까운 적을 찾아서 방향을 계산.
/// 추후 계산한 방향으로 투사체를 발사.
/// </summary>
public class AutoAttackController : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool canFireProjectile = true;

    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float attackInterval = 1.0f;

    [SerializeField] private int projectileDamage = 1;
    [SerializeField] private int damageIncreaseAmount = 1;
    [SerializeField] private int maxProjectileDamage = 99;

    [SerializeField] private float minAttackInterval = 0.15f;
    [SerializeField] private float cooldownMultiplier = 0.9f;

    [SerializeField] private int projectileCount = 1;
    [SerializeField] private int projectileCountIncreaseAmount = 1;
    [SerializeField] private int maxProjectileCount = 5;
    [SerializeField] private float projectileSpreadAngle = 12.0f;

    [SerializeField] private float projectileSpeedMultiplier = 1.0f;
    [SerializeField] private float speedMultiplierIncreaseAmount = 0.15f;
    [SerializeField] private float maxProjectileSpeedMultiplier = 2.5f;

    private float attackTimer = 0.0f;
    private Vector2 lastAttackDirection = Vector2.right;
    private Collider2D currentTargetCollider;

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackInterval)
        {
            attackTimer = 0.0f;

            // 공격 시도.
            TryAutoAttack();
        }
    }

    void TryAutoAttack()
    {
        currentTargetCollider = FindNearestEnemy();

        if(currentTargetCollider == null)
        {
            return;
        }

        Vector2 targetPosition = currentTargetCollider.transform.position;
        Vector2 originPosition = transform.position;

        lastAttackDirection = (targetPosition - originPosition).normalized;

        //Debug.DrawRay(transform.position, (targetPosition - originPosition), Color.green, 1.0f);

        FireProjectile(lastAttackDirection);
    }

    void FireProjectile(Vector2 fireDirection)
    {
        if(canFireProjectile == false)
        {
            return;
        }

        if(projectilePrefab == null)
        {
            return;
        }

        if(firePoint == null)
        {
            return;
        }

        Vector2 centerDirection = fireDirection.normalized;

        if(projectileCount == 1)
        {
            SpawnProjectile(centerDirection);
        }
        else
        {
            // 여러 발을 중심 방향 기준으로 좌우 대칭 배치를 하기 위한 시작 각도를 계산.
            float startAngle = -projectileSpreadAngle * (projectileCount - 1) * 0.5f;

            for(int i=0; i<projectileCount; ++i)
            {
                // 투사체가 중심 방향에서 얼마나 회전할지 계산.
                float angleOffset = startAngle + (projectileSpreadAngle * i);

                // 중심 방향을 angleOffset만큼 회전시켜서 실제 방향을 만든다.
                Vector2 rotatedDirection = RotateDirection(centerDirection, angleOffset);

                SpawnProjectile(rotatedDirection);
            }
        }
    }

    Vector2 RotateDirection(Vector2 direction, float angledegrees)
    {
        // Quaternion.Euler : 오브젝트의 회전 각도 값을 지정하는 함수.
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angledegrees);

        // Quaternion을 방향 벡터에 곱해서 회전된 방향을 계산.
        Vector2 rotatedDirection = rotation * direction;

        return rotatedDirection.normalized;
    }

    void SpawnProjectile(Vector2 fireDirection)
    {
        Vector3 spawnPosition = firePoint.position + (Vector3)fireDirection.normalized;

        Projectile projectile = Instantiate(projectilePrefab, spawnPosition,
            Quaternion.identity);

        if (projectile != null)
        {
            projectile.SetDamage(projectileDamage);
            projectile.SetSpeedMultiplier(projectileSpeedMultiplier);
            projectile.Initialize(fireDirection);
        }
    }

    public void ApplyDamageUpgrade()
    {
        int nextDamage = projectileDamage + damageIncreaseAmount;
        projectileDamage = Mathf.Min(nextDamage, maxProjectileDamage);
    }

    public void ApplyCooldownUpgrade()
    {
        float nextInterval = attackInterval * cooldownMultiplier;
        attackInterval = Mathf.Max(nextInterval, minAttackInterval);
    }

    public void ApplyBalancedAttackUpgrade()
    {
        ApplyDamageUpgrade();
        ApplyCooldownUpgrade();
    }

    public void ApplyProjectileCountUpgrade()
    {
        int nextCount = projectileCount + projectileCountIncreaseAmount;
        projectileCount = Mathf.Min(nextCount, maxProjectileCount);
    }

    public void ApplyProjectileSpeedUpgrade()
    {
        float nextMultiplier = projectileSpeedMultiplier + speedMultiplierIncreaseAmount;
        projectileSpeedMultiplier = Mathf.Min(nextMultiplier, maxProjectileSpeedMultiplier);
    }

    public void ApplyProjectilePatternUpgrade()
    {
        ApplyProjectileCountUpgrade();
        ApplyProjectileSpeedUpgrade();
    }

    Collider2D FindNearestEnemy()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);

        Collider2D nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        for(int i=0; i<enemyColliders.Length; ++i)
        {
            Collider2D enemyCollider = enemyColliders[i];

            float distance = Vector2.Distance(transform.position, enemyCollider.transform.position);

            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemyCollider;
            }
        }

        return nearestEnemy;
    }
}
