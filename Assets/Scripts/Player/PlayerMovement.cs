using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Rigidbody2D playerRigidbody;

    private Vector2 inputDirection = Vector2.zero;
    private Vector2 moveDirection = Vector2.zero;

    private bool isMoving = false;

    private float moveSpeedBonus = 0.0f;

    private void Awake()
    {
        if(playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsPlaying == false)
        {
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(horizontalInput, verticalInput);

        // 백터의 정규화.
        // 벡터의 길이를 1로 만들어서 방향 정보만 남기는 것.
        inputDirection = inputDirection.normalized;

        moveDirection = inputDirection;

        isMoving = moveDirection != Vector2.zero;
    }

    void FixedUpdate()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsPlaying == false)
        {
            return;
        }

        playerRigidbody.linearVelocity = moveDirection * (moveSpeed + moveSpeedBonus);
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public void AddMoveSpeedBonus(float bonus)
    {
        moveSpeedBonus += bonus;
    }

    public void RemoveMoveSpeedBonus(float bonus)
    {
        moveSpeedBonus -= bonus;

        if(moveSpeedBonus < 0.0f)
        {
            moveSpeedBonus = 0.0f;
        }
    }
}
