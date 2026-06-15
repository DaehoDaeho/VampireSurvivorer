using UnityEngine;

public class PlayerExperienceCollector : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;

    [SerializeField] private int currentExperience = 0;

    // 1레벨에서 2레벨로 올라가기 위해 필요한 기본 경험치.
    [SerializeField] private int baseExperienceToNextLevel = 5;

    // 레벨이 오를 때마다 다음 필요 경험치를 얼마나 늘릴지 정하는 값.
    [SerializeField] private int experienceIncreasePerLevel = 3;

    // 현재 레벨에서 다음 레벨까지 필요한 경험치.
    [SerializeField] private int experienceToNextLevel = 0;

    [SerializeField] private LevelUpSelectionUI levelUpSelectionUI;

    public int CurrentExperience
    {
        get { return currentExperience; }
    }

    public int CurrentLevel
    {
        get { return currentLevel; }
    }

    public int ExperienceToNextLevel
    {
        get { return experienceToNextLevel; }
    }

    private void Awake()
    {
        experienceToNextLevel = baseExperienceToNextLevel;
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;

        // 레벨업 시도.
        TryLevelUp();
    }

    void TryLevelUp()
    {
        // 연속 레벨업이 가능한 경우를 위해 경험치가 충분할 동안 반복 레벨업 처리.
        while (currentExperience >= experienceToNextLevel)
        {
            // 레벨업을 하고 남은 경험치를 보존.
            currentExperience -= experienceToNextLevel;

            // 레벨업.
            LevelUp();
        }
    }

    void LevelUp()
    {
        //int a = 0;
        //int b = a++; // -> 후위 증가.
        // b=0, a=1

        //int c = 0;
        //int d = ++c; // -> 전의 증가.
        // c=1, d=1 

        currentLevel++;

        experienceToNextLevel += experienceIncreasePerLevel;

        // 레벨업 선택 UI 열기.
        OpenLevelUpSelection();
    }

    void OpenLevelUpSelection()
    {
        if(levelUpSelectionUI != null)
        {
            levelUpSelectionUI.OpenSelection(currentLevel);
        }
    }

    public float GetExperienceProgress01()
    {
        if(experienceToNextLevel <= 0)
        {
            return 0.0f;
        }

        return (float)currentExperience / experienceToNextLevel;
    }
}
