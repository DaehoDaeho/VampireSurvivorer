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
    [SerializeField] private string[] optionNames = { "Attack Speed Up", "Projectile Speed Up", "Defense Up" };

    private bool isOpen = false;

    void CloseSelection()
    {
        isOpen = false;

        rootPanel.SetActive(false);
    }

    void OnOptionSelected(int optionIndex)
    {
        string selectedName = optionNames[optionIndex];

        Debug.Log("레벨업 선택 : " + selectedName);

        CloseSelection();
    }

    void RefreshOptionTexts(int level)
    {
        for(int i=0; i<optionText.Length; ++i)
        {
            optionText[i].text = optionNames[i] + " / Lv." + level;
        }
    }

    public void OpenSelection(int level)
    {
        if (isOpen == true)
        {
            return;
        }

        isOpen = true;

        rootPanel.SetActive(true);

        RefreshOptionTexts(level);
    }

    void ConnectButtonEvent()
    {
        for(int i=0; i<optionButton.Length; ++i)
        {
            int capturedIndex = i;
            optionButton[i].onClick.RemoveAllListeners();
            optionButton[i].onClick.AddListener(() => OnOptionSelected(capturedIndex));
        }
    }

    private void Awake()
    {
        CloseSelection();
        ConnectButtonEvent();
    }
}
