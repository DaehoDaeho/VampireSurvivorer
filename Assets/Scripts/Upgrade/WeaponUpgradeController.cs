using UnityEngine;

/// <summary>
/// 레벨업 선택 결과를 실제 무기 강화로 연결하는 역할.
/// </summary>
public class WeaponUpgradeController : MonoBehaviour
{
    [SerializeField] private AutoAttackController autoAttackController;

    public void ApplyUpgrade(WeaponUpgradeData upgradeData)
    {
        if(upgradeData.upgradeType == UpgradeType.Damage)
        {
            autoAttackController.ApplyDamageUpgrade(upgradeData.intValue);
        }
        else if(upgradeData.upgradeType == UpgradeType.Cooldown)
        {
            autoAttackController.ApplyCooldownUpgrade(upgradeData.floatValue);
        }
        else if (upgradeData.upgradeType == UpgradeType.ProjectileCount)
        {
            autoAttackController.ApplyProjectileCountUpgrade(upgradeData.intValue);
        }
        else if (upgradeData.upgradeType == UpgradeType.ProjectileSpeed)
        {
            autoAttackController.ApplyProjectileSpeedUpgrade(upgradeData.floatValue);
        }
        else if (upgradeData.upgradeType == UpgradeType.ProjectilePattern)
        {
            autoAttackController.ApplyProjectilePatternUpgrade(upgradeData.intValue, upgradeData.floatValue);
        }
    }
}
