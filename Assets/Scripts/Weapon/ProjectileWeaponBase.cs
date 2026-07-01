using UnityEngine;

public class ProjectileWeaponBase : MonoBehaviour
{
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected Transform firePoint;

    [SerializeField] protected float cooldown = 1.0f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float projectileSpeed = 8.0f;
    [SerializeField] protected int projectileHitCount = 1;

    private float fireTimer;

    // Update is called once per frame
    protected void Update()
    {
        if(GameStateManager.Instance != null && GameStateManager.Instance.IsPlaying == false)
        {
            return;
        }

        fireTimer += Time.deltaTime;
        if(fireTimer >= cooldown)
        {
            fireTimer = 0.0f;
            Fire();
        }
    }

    protected Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    protected void FireProjectile(Vector2 direction)
    {
        if(projectilePrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Projectile projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.SetUp(direction, damage, projectileSpeed, projectileHitCount);
    }

    protected virtual void Fire()
    {

    }
}
