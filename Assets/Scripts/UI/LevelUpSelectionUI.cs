using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 레벨업 시 표시되는 선택 UI를 관리하는 역할.
/// </summary>
public class LevelUpSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private Button[] optionButton;
    [SerializeField] private TMP_Text[] optionText;
    
    [SerializeField] private WeaponUpgradeData[] upgradeOptions;
    [SerializeField] private WeaponUpgradeController weaponUpgradeController;

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

        RefreshOptionButtons();

        Time.timeScale = 0.0f;
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
            bool hasData = upgradeOptions != null && i < upgradeOptions.Length && upgradeOptions[i] != null;

            optionButton[i].gameObject.SetActive(hasData);

            if(hasData == false)
            {
                continue;
            }

            optionText[i].text = upgradeOptions[i].optionName;
        }
    }

    public void SelectOption(int optionIndex)
    {
        WeaponUpgradeData selectedUpgrade = upgradeOptions[optionIndex];

        weaponUpgradeController.ApplyUpgrade(selectedUpgrade);

        CloseSelection();
    }

    private void Awake()
    {
        CloseSelection();
        ConnectButtonEvent();
    }
}
