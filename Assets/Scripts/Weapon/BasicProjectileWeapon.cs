using UnityEngine;

public class BasicProjectileWeapon : ProjectileWeaponBase
{
    protected override void Fire()
    {
        Transform target = FindNearestEnemy();

        if(target == null)
        {
            return;
        }

        Vector2 direction = target.position - transform.position;
        FireProjectile(direction);
    }
}
