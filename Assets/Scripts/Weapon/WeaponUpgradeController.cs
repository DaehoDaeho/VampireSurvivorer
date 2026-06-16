using UnityEngine;

/// <summary>
/// 레벨업 선택 결과를 실제 무기 강화로 연결하는 역할.
/// </summary>
public class WeaponUpgradeController : MonoBehaviour
{
    [SerializeField] private AutoAttackController autoAttackController;

    public void ApplyUpgrade(int optionIndex)
    {
        if(optionIndex == 0)
        {
            autoAttackController.ApplyDamageUpgrade();
        }
        else if(optionIndex == 1)
        {
            autoAttackController.ApplyCooldownUpgrade();
        }
        else if(optionIndex == 2)
        {
            autoAttackController.ApplyBalancedAttackUpgrade();
        }
    }
}
