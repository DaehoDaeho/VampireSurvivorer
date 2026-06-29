using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private float destroyDelay = 0.2f;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private Rigidbody2D bodyRigidbody;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private ExperienceDropper experienceDropper;
    [SerializeField] private EnemyItemDropper enemyItemDropper;

    private int currentHealth = 0;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if(isDead == true)
        {
            return;
        }

        if(damageAmount <= 0)
        {
            return;
        }

        currentHealth -= damageAmount;

        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log(gameObject.name + " HP: " + currentHealth);

        if(currentHealth == 0)
        {
            Die();
        }    
    }

    void Die()
    {
        if(isDead == true)
        {
            return;
        }

        isDead = true;

        if(KillCountTracker.Instance != null)
        {
            KillCountTracker.Instance.AddKillCount();
        }

        DisableComponentsAfterDeath();

        if(destroyOnDeath == true)
        {
            Invoke("DropExperienceAndItem", destroyDelay);
            Destroy(gameObject, destroyDelay);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //void OnDestroy()
    //{
    //    DropExperience();
    //}

    void DropExperienceAndItem()
    {
        if(experienceDropper != null)
        {
            experienceDropper.DropExperience();
        }

        if(enemyItemDropper != null)
        {
            enemyItemDropper.TryDropItem();
        }
    }

    void DisableComponentsAfterDeath()
    {
        if(enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }

        if(bodyCollider != null)
        {
            bodyCollider.enabled = false;
        }

        if(bodyRigidbody != null)
        {
            bodyRigidbody.linearVelocity = Vector2.zero;
        }
    }
}
