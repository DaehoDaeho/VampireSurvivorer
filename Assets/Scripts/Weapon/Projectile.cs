using UnityEngine;

/// <summary>
/// 투사체 이동과 자동 파괴,
/// 추후 적에게 데미지를 주는 역할.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float lifeTime = 3.0f;

    private Vector2 moveDirection = Vector2.right;
    private float lifeTimer = 0.0f;

    private bool isInitialized = false;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;

        lifeTimer = 0.0f;

        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInitialized == false)
        {
            return;
        }

        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
        transform.position += moveAmount;

        lifeTimer += Time.deltaTime;
        if(lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
