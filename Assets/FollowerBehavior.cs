using UnityEngine;

public class FollowerBehavior : MonoBehaviour
{
    public Transform player; // Nhân vật chính
    public float followSpeed = 2f; // Tốc độ di chuyển của Object
    public float safeDistance = 1.5f; // Khoảng cách an toàn giữa Object và nhân vật
    public int minDiligence = 0; // Giá trị Diligence tối thiểu để Object đi theo
    public int maxDiligence = 30; // Giá trị Diligence tối đa để Object đi theo
    public int happinessPenalty = 20; // Lượng Happiness bị trừ nếu không được Object đi theo

    private bool isActivated = false; // Chỉ kích hoạt khi nhân vật chạm vào

    private void Update()
    {
        if (isActivated)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // Nếu khoảng cách lớn hơn safeDistance, di chuyển mượt mà về phía nhân vật
            if (distance > safeDistance)
            {
                Vector3 targetPosition = player.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Khi nhân vật chạm vào object
        if (collision.CompareTag("Player"))
        {
            if (IsWithinDiligenceRange())
            {
                isActivated = true; // Object sẽ bắt đầu đi theo người chơi
                Debug.Log("Object follows player!");
            }
            else
            {
                // Nếu không đủ Diligence, trừ Happiness (chỉ trừ 1 lần)
                PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenalty, 0);

                // Cập nhật UI nếu có StatsDisplay
                StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStatsUI();
                }

                Debug.Log($"Diligence too low! Happiness decreased to {PlayerStats.Happiness}");
            }
        }
    }

    private bool IsWithinDiligenceRange()
    {
        // Kiểm tra nếu Diligence nằm trong khoảng [minDiligence, maxDiligence]
        return PlayerStats.Diligence >= minDiligence && PlayerStats.Diligence <= maxDiligence;
    }
}
