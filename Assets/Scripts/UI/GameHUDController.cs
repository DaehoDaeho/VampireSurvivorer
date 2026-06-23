using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHUDController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerExperienceCollector experienceCollector;

    [SerializeField] private Image healthBar;
    [SerializeField] TMP_Text healthText;

    [SerializeField] private Image experienceBar;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text experienceText;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text killCountText;

    private float elapsedTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        UpdateExperienceUI();
        UpdateTimeUI();
        UpdateKillCountUI();
    }

    void UpdateExperienceUI()
    {
        if(experienceCollector != null)
        {
            if(experienceBar != null)
            {
                experienceBar.fillAmount = experienceCollector.GetExperienceProgress01();
            }

            if(levelText != null)
            {
                levelText.text = "Lv. " + experienceCollector.CurrentLevel;
            }

            if(experienceText != null)
            {
                experienceText.text = experienceCollector.CurrentExperience + " / " + experienceCollector.ExperienceToNextLevel;
            }
        }
    }

    void UpdateTimeUI()
    {
        if(timeText != null)
        {
            // Mathf.FloorToInt : 내림 처리 후 int 값을 반환.
            int totalSeconds = Mathf.FloorToInt(elapsedTime);
            int minute = totalSeconds / 60;
            int second = totalSeconds % 60;
            timeText.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }

    void UpdateKillCountUI()
    {
        if(killCountText != null && KillCountTracker.Instance != null)
        {
            killCountText.text = "Kill " + KillCountTracker.Instance.KillCount;
        }
    }
}
