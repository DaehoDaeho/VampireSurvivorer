using UnityEngine;

public class ExperienceDropper : MonoBehaviour
{
    [SerializeField] private ExperienceGem experienceGemPrefab;

    [SerializeField] private float dropRadius = 0.25f;

    public void DropExperience()
    {
        if(experienceGemPrefab != null)
        {
            Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
            Vector3 dropPosition = transform.position + (Vector3)randomOffset;

            Instantiate(experienceGemPrefab, dropPosition, Quaternion.identity);
        }
    }
}
