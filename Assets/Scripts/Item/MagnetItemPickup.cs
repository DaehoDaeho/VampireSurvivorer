using UnityEngine;

public class MagnetItemPickup : MonoBehaviour
{
    [SerializeField] private float magnetDuration = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == false)
        {
            return;
        }

        ExperienceMagnetController magnetController = collision.GetComponent<ExperienceMagnetController>();

        if(magnetController != null)
        {
            magnetController.Activate(magnetDuration);
        }

        Destroy(gameObject);
    }
}
