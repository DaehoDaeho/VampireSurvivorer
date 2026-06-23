using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Game/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public List<WeaponUpgradeData> upgradeOptions;

    public List<WeaponUpgradeData> GetUpgradeOptionsClone()
    {
        List<WeaponUpgradeData> clone = new List<WeaponUpgradeData>(upgradeOptions);
        return clone;
    }
}
