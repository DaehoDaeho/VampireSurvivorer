using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float followSpeed = 8.0f;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);

    [SerializeField] private bool useClamp = false;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    private void LateUpdate()
    {
        if(targetTransform == null)
        {
            return;
        }

        // 벡터의 덧셈.
        // x성분끼리, y성분끼리, z성분끼리 더하면 됨.
        Vector3 targetPosition = targetTransform.position + offset;
        Vector3 nextPosition = Vector3.Lerp(transform.position, targetPosition,
            followSpeed * Time.deltaTime);

        if(useClamp == true)
        {
            // Mathf.Clamp : 값이 최소/최대값 범위를 벗어나지 않게 보정시켜주는 함수.
            nextPosition.x = Mathf.Clamp(nextPosition.x, minPosition.x, maxPosition.x);

            nextPosition.y = Mathf.Clamp(nextPosition.y, minPosition.y, maxPosition.y);
        }

        transform.position = nextPosition;
    }
}
