using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private EnemyProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRange = 4.0f;
    [SerializeField] private float attackInterval = 1.5f;
    [SerializeField] private float projectileSpeed = 5.0f;
    [SerializeField] private int damage = 1;

    private Transform playerTransform;
    private float attackTimer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if(distanceToPlayer <= attackRange && attackTimer >= attackInterval)
        {
            FireProjectile();
            attackTimer = 0.0f;
        }
    }

    void FireProjectile()
    {
        Vector2 direction = (playerTransform.position - firePoint.position).normalized;

        // 총알 생성.
        EnemyProjectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        if(projectile != null)
        {
            projectile.Initialize(direction, projectileSpeed, damage);
        }
    }
}
