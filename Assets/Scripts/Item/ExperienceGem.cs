using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    [SerializeField] private int experienceAmount = 1;
    [SerializeField] private string playerTag = "Player";

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
}
