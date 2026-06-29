using UnityEngine;

public class SpeedBoostItemPickup : MonoBehaviour
{
    [SerializeField] private float boostDuration = 4.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == false)
        {
            return;
        }

        PlayerSpeedBoost playerSpeedBoost = collision.GetComponent<PlayerSpeedBoost>();

        if(playerSpeedBoost != null)
        {
            playerSpeedBoost.Activate(boostDuration);
        }

        Destroy(gameObject);
    }
}
