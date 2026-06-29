using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [SerializeField] private int experienceAmount = 1;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private ExperienceMagnetController magnetController;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private float magnetMoveSpeed = 8.0f;
    [SerializeField] private float magnetDetectRange = 6.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) == true)
        {
            PlayerExperienceCollector collector = collision.GetComponent<PlayerExperienceCollector>();

            if(collector != null)
            {
                collector.AddExperience(experienceAmount);
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerTransform = player.transform;
            magnetController = player.GetComponent<ExperienceMagnetController>();            
        }
    }

    private void Update()
    {
        if(playerTransform != null && magnetController != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (magnetController.IsMagnetActive == true && distanceToPlayer <= magnetDetectRange)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)(direction * magnetMoveSpeed * Time.deltaTime);
            }
        }
    }
}
