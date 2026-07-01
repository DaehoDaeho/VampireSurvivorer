using UnityEngine;

public class PiercingProjectileWeapon : ProjectileWeaponBase
{
    protected override void Fire()
    {
        Transform target = FindNearestEnemy();

        if(target == null)
        {
            return;
        }

        projectileHitCount = Mathf.Max(3, projectileHitCount);

        Vector2 direction = target.position - transform.position;
        FireProjectile(direction);
    }
}
