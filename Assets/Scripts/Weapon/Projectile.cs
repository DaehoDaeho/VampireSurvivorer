using UnityEngine;

/// <summary>
/// 투사체 이동과 자동 파괴,
/// 추후 적에게 데미지를 주는 역할.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float lifeTime = 3.0f;

    [SerializeField] private int damage = 1;

    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private bool destroyOnHit = true;

    private Vector2 moveDirection = Vector2.right;
    private float lifeTimer = 0.0f;

    private bool isInitialized = false;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;

        lifeTimer = 0.0f;

        isInitialized = true;

        RotateToMoveDirection();
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void RotateToMoveDirection()
    {
        // Mathf.Atan2의 반환값은 각도가 아니라 라디안(Radian)값이다.
        // 라디안 값을 각도로 변환시켜주는 과정이 필요하다.
        // Mathf.Rad2Deg (Radian To Degree) : 라디안을 각도로 변환.
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        // Quaternion.Euler() : 오브젝트를 지정된 각도만큼 회전시켜주는 함수.
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);

            if(destroyOnHit == true)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
