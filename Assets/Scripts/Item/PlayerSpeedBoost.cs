using UnityEngine;

public class PlayerSpeedBoost : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float speedBonus = 2.0f;

    private float remainTime = 0.0f;
    private bool isBoostActive = false;

    public void Activate(float duration)
    {
        if(playerMovement == null)
        {
            return;
        }

        if(isBoostActive == false)
        {
            playerMovement.AddMoveSpeedBonus(speedBonus);
            isBoostActive = true;
        }

        remainTime = Mathf.Max(remainTime, duration);
    }

    private void Update()
    {
        if(isBoostActive == false)
        {
            return;
        }

        remainTime -= Time.deltaTime;

        if(remainTime <= 0.0f)
        {
            remainTime = 0.0f;
            isBoostActive = false;
            playerMovement.RemoveMoveSpeedBonus(speedBonus);
        }
    }
}
