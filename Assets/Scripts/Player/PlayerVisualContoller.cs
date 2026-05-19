using UnityEngine;

public class PlayerVisualContoller : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private string moveParameter = "Move";

    private Vector2 currentMoveDirection = Vector2.zero;
    private Vector2 lastLookDirection = Vector2.right;

    private void Awake()
    {
        if(playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if(animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentMoveDirection = playerMovement.GetMoveDirection();
        if(currentMoveDirection != Vector2.zero)
        {
            lastLookDirection = currentMoveDirection;
        }

        UpdateSpriteDirection();
        UpdateAnimatorParameter();
    }

    void UpdateSpriteDirection()
    {
        if(lastLookDirection.x < 0.0f)
        {
            spriteRenderer.flipX = true;
        }
        else if(lastLookDirection.x > 0.0f)
        {
            spriteRenderer.flipX = false;
        }
    }

    void UpdateAnimatorParameter()
    {
        bool isMoving = playerMovement.IsMoving();

        animator.SetBool(moveParameter, isMoving);
    }
}
