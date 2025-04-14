using UnityEngine;

public class MoneyObject : MonoBehaviour
{
    public int minMoney = 5; // Số tiền tối thiểu có thể kiếm được
    public int maxMoney = 20; // Số tiền tối đa có thể kiếm được
    public float chanceToEarn = 0.5f; // 50% tỉ lệ nhận tiền
    public int selfishIncreaseAmount = 5; // Số điểm Selfishness tăng khi kiếm được tiền
    public int happinessIncreaseAmount = 15; // Số điểm Happiness tăng khi kiếm được tiền

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu người chơi chạm vào
        if (collision.CompareTag("Player"))
        {
            // Xác suất ngẫu nhiên để nhận tiền
            if (Random.value <= chanceToEarn) // Random.value trả về số từ 0 đến 1
            {
                // Tính số tiền nhận được
                int earnedMoney = Random.Range(minMoney, maxMoney + 1);

                // Thêm tiền cho người chơi
                PlayerMoney.Instance.AddMoney(earnedMoney);

                // Tăng chỉ số Selfishness
                PlayerStats.Selfishness += selfishIncreaseAmount;

                // Tăng chỉ số Happiness
                PlayerStats.Happiness = Mathf.Min(PlayerStats.Happiness + happinessIncreaseAmount, 100);

                // Cập nhật UI nếu có StatsDisplay
                StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStatsUI();
                }

                // Debug thông báo
                Debug.Log($"Player earned {earnedMoney} money! Selfishness: {PlayerStats.Selfishness}, Happiness: {PlayerStats.Happiness}");
            }
            else
            {
                Debug.Log("No money this time!");
            }

            // Hủy Object sau khi chạm
            Destroy(gameObject);
        }
    }
}
