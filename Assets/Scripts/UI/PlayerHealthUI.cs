using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar; // 체력 표시 Slider

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthBar == null)
        {
            return;
        }

        float healthRate = 0.0f; // healthBar에 넣을 0~1 사이 비율

        if (maxHealth > 0)
        {
            healthRate = (float)currentHealth / maxHealth;
        }

        healthBar.fillAmount = healthRate;
    }
}
