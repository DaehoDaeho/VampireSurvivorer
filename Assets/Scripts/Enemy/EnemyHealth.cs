using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth = 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if(damageAmount <= 0)
        {
            return;
        }

        currentHealth -= damageAmount;

        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log(gameObject.name + " HP: " + currentHealth);
    }
}
