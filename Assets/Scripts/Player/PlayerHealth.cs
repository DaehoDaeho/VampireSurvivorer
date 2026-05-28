using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5; // 최대 체력
    [SerializeField] private float invincibilityDuration = 1.0f; // 무적 시간
    [SerializeField] private SpriteRenderer playerSpriteRenderer; // 깜빡임 대상
    [SerializeField] private PlayerHealthUI playerHealthUI; // 체력 UI 담당
    [SerializeField] private bool invincible = false;

    private int currentHealth = 0; // 현재 체력
    private bool isInvincible = false; // 무적 상태 여부
    private bool isDead = false; // 사망 여부

    private void Awake()
    {
        currentHealth = maxHealth;

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        UpdateHealthDisplay();
    }

    public void TakeDamage(int damageAmount)
    {
        if(invincible == true)
        {
            return;
        }

        if (damageAmount <= 0)
        {
            return;
        }

        if (isDead == true)
        {
            return;
        }

        if (isInvincible == true)
        {
            return;
        }
        currentHealth -= damageAmount; // 받은 데미지만큼 체력 감소
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력 제한
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityRoutine());
    }

    public void Heal(int healAmount)
    {
        if (healAmount <= 0)
        {
            return;
        }

        if (isDead == true)
        {
            return;
        }

        currentHealth += healAmount; // 회복량만큼 체력 증가
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthDisplay();
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        float elapsedTime = 0.0f; // 무적 시간 누적값

        while (elapsedTime < invincibilityDuration)
        {
            ToggleSpriteVisible();
            elapsedTime += 0.12f;
            yield return new WaitForSeconds(0.12f);
        }

        SetSpriteVisible(true);
        isInvincible = false;
    }

    private void UpdateHealthDisplay()
    {
        if (playerHealthUI != null)
        {
            playerHealthUI.UpdateHealth(currentHealth, maxHealth);
        }
    }

    private void ToggleSpriteVisible()
    {
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.enabled = playerSpriteRenderer.enabled == false;
        }
    }

    private void SetSpriteVisible(bool isVisible)
    {
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.enabled = isVisible;
        }
    }

    private void Die()
    {
        isDead = true;
        SetSpriteVisible(true);
    }
}
