using UnityEngine;

public class FollowerBehavior : MonoBehaviour
{
    public Transform player; // Nhân vật chính
    public float followSpeed = 2f; // Tốc độ di chuyển của Object
    public float safeDistance = 1.5f; // Khoảng cách an toàn giữa Object và nhân vật
    public int minDiligence = 0; // Giá trị Diligence tối thiểu để Object đi theo
    public int maxDiligence = 30; // Giá trị Diligence tối đa để Object đi theo

    private bool isActivated = false; // Trạng thái: chỉ bắt đầu di chuyển khi kích hoạt
    private Vector3 velocity = Vector3.zero; // Tốc độ dùng cho SmoothDamp

    private void Update()
    {
        // Kiểm tra nếu Object đã được kích hoạt
        if (isActivated && IsWithinDiligenceRange())
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // Nếu khoảng cách lớn hơn safeDistance, di chuyển mượt mà về phía nhân vật
            if (distance > safeDistance)
            {
                Vector3 targetPosition = player.position;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f);
            }
        }
    }

    private bool IsWithinDiligenceRange()
    {
        // Kiểm tra nếu Diligence nằm trong khoảng [minDiligence, maxDiligence]
        return PlayerStats.Diligence >= minDiligence && PlayerStats.Diligence <= maxDiligence;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kích hoạt khi nhân vật chạm vào
        if (collision.CompareTag("Player"))
        {
            isActivated = true;
            Debug.Log("Object activated by player!");
        }
    }
}
