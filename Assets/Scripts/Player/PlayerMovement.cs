using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Rigidbody2D playerRigidbody;

    private Vector2 inputDirection = Vector2.zero;
    private Vector2 moveDirection = Vector2.zero;

    private bool isMoving = false;

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
        playerRigidbody.linearVelocity = moveDirection * moveSpeed;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
