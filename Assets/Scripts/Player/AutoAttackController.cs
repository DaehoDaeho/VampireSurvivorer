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

        Projectile projectile = Instantiate(projectilePrefab, firePoint.position,
            Quaternion.identity);

        if(projectile != null)
        {
            projectile.SetDamage(projectileDamage);
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
