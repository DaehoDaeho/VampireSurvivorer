using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private int contactDamage = 1;
    [SerializeField] private float damageInterval = 0.6f;
    [SerializeField] private string targetTag = "Player";

    private float damageTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (damageTimer > 0.0f)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer < 0.0f)
            {
                damageTimer = 0.0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) == false)
        {
            return;
        }

        if (damageTimer > 0.0f)
        {
            return;
        }

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        playerHealth.TakeDamage(contactDamage);
        damageTimer = damageInterval;
    }
}
