using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// 레벨업 시 표시되는 선택 UI를 관리하는 역할.
/// </summary>
public class LevelUpSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private Button[] optionButton;
    [SerializeField] private TMP_Text[] optionText;

    [SerializeField] private UpgradeData upgrades;
    [SerializeField] private List<WeaponUpgradeData> upgradeOptions;
    [SerializeField] private WeaponUpgradeController weaponUpgradeController;

    private readonly List<WeaponUpgradeData> currentUpgradeOptions = new List<WeaponUpgradeData>();

    private bool isOpen = false;

    void CloseSelection()
    {
        isOpen = false;

        Time.timeScale = 1.0f;

        rootPanel.SetActive(false);
    }

    public void OpenSelection(int level)
    {
        if (isOpen == true)
        {
            return;
        }

        isOpen = true;

        rootPanel.SetActive(true);

        PickupRandomOptions();
        RefreshOptionButtons();

        Time.timeScale = 0.0f;
    }

    void PickupRandomOptions()
    {
        currentUpgradeOptions.Clear();

        List<WeaponUpgradeData> candidateList = new List<WeaponUpgradeData>();

        for(int i=0; i<upgradeOptions.Count; ++i)
        {
            candidateList.Add(upgradeOptions[i]);
        }

        int displayCount = optionButton.Length;

        while(currentUpgradeOptions.Count < displayCount && candidateList.Count > 0)
        {
            int randomIndex = Random.Range(0, candidateList.Count);
            WeaponUpgradeData selectedData = candidateList[randomIndex];
            currentUpgradeOptions.Add(selectedData);
            candidateList.RemoveAt(randomIndex);
        }
    }

    void ConnectButtonEvent()
    {
        for(int i=0; i<optionButton.Length; ++i)
        {
            int capturedIndex = i;
            optionButton[i].onClick.RemoveAllListeners();
            optionButton[i].onClick.AddListener(() => SelectOption(capturedIndex));
        }
    }

    void RefreshOptionButtons()
    {
        for(int i=0; i<optionButton.Length; ++i)
        {
            bool hasData = i < currentUpgradeOptions.Count;
            optionButton[i].gameObject.SetActive(hasData);

            if(hasData == false)
            {
                continue;
            }

            optionText[i].text = currentUpgradeOptions[i].optionName;
        }
    }

    public void SelectOption(int optionIndex)
    {
        WeaponUpgradeData selectedUpgrade = currentUpgradeOptions[optionIndex];

        weaponUpgradeController.ApplyUpgrade(selectedUpgrade);

        CloseSelection();
    }

    private void Awake()
    {
        upgradeOptions = upgrades.GetUpgradeOptionsClone();

        CloseSelection();
        ConnectButtonEvent();
    }
}
