using System.IO.Pipes;
using UnityEngine;

public class ShotgunWeapon : ProjectileWeaponBase
{
    [SerializeField] private int projectileCount = 5;
    [SerializeField] private float spreadAngle = 45.0f;

    protected override void Fire()
    {
        Transform target = FindNearestEnemy();

        if(target == null)
        {
            return;
        }

        Vector2 centerDirection = (target.position - transform.position).normalized;

        float startAngle = -spreadAngle * (projectileCount - 1) * 0.5f;

        for (int i = 0; i < projectileCount; ++i)
        {
            // 투사체가 중심 방향에서 얼마나 회전할지 계산.
            float angleOffset = startAngle + (spreadAngle * i);

            // 중심 방향을 angleOffset만큼 회전시켜서 실제 방향을 만든다.
            Vector2 rotatedDirection = Quaternion.Euler(0.0f, 0.0f, angleOffset) * centerDirection;

            FireProjectile(rotatedDirection);
        }
    }
}
