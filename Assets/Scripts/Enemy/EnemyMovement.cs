using UnityEngine;

/// <summary>
/// 적 캐릭터가 Player를 향해 이동하는 역할.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float stopDistance = 0.1f;

    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private bool flipSpriteByDirection = true;

    private Vector2 moveDirection = Vector2.zero;
    private bool isMoving = false;

    public bool IsMoving()
    {
        return isMoving;
    }

    private void Awake()
    {
        if(enemyRigidbody == null)
        {
            enemyRigidbody = GetComponent<Rigidbody2D>();
        }

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        if(targetTransform == null)
        {
            //GameObject playerObject = GameObject.Find("Player");
            //GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            PlayerMovement playerObject = GameObject.FindAnyObjectByType<PlayerMovement>();
            if(playerObject != null)
            {
                targetTransform = playerObject.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        if(targetTransform == null)
        {
            // 멈추는 코드.
            StopMoving();
            return;
        }

        Vector2 enemyPosition = enemyRigidbody.position;
        Vector2 targetPosition = targetTransform.position;
        Vector2 directionToTarget = targetPosition - enemyPosition;

        // magnitude : 벡터의 크기(거리 용도로 사용 가능)
        float distanceToTarget = directionToTarget.magnitude;
        if(distanceToTarget <= stopDistance)
        {
            StopMoving();
            return;
        }

        moveDirection = directionToTarget.normalized;
        enemyRigidbody.linearVelocity = moveDirection * moveSpeed;
        isMoving = true;

        UpdateVisualDirection();
    }

    void StopMoving()
    {
        if(enemyRigidbody != null)
        {
            enemyRigidbody.linearVelocity = Vector2.zero;
        }

        moveDirection = Vector2.zero;
        isMoving = false;
    }

    void UpdateVisualDirection()
    {
        if(flipSpriteByDirection == false)
        {
            return;
        }

        if(spriteRenderer == null)
        {
            return;
        }

        if(moveDirection.x != 0.0f)
        {
            spriteRenderer.flipX = moveDirection.x < 0.0f;
        }
    }
}
