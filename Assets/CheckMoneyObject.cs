using UnityEngine;

public class CheckMoneyObject : MonoBehaviour
{
    public string followerTag = "Follower"; // Tag để nhận diện tất cả các Follower Object
    public int happinessPenalty = 40; // Lượng Happiness bị trừ nếu không còn follower

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu người chơi chạm vào CheckMoneyObject
        if (collision.CompareTag("Player"))
        {
            // Kiểm tra số tiền của người chơi
            if (PlayerMoney.Instance.money <= 45)
            {
                // Tìm tất cả các Follower Object trong scene bằng tag
                GameObject[] followers = GameObject.FindGameObjectsWithTag(followerTag);

                if (followers.Length > 0)
                {
                    foreach (GameObject follower in followers)
                    {
                        // Hủy từng Follower Object
                        Destroy(follower);
                        Debug.Log("Destroyed follower: " + follower.name);
                    }

                    // Trừ điểm Happiness vì mất follower
                    PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenalty, 0);

                    // ✅ Cập nhật lại trạng thái không còn vợ
                    PlayerStats.HasWife = false;

                    // Cập nhật UI nếu có StatsDisplay
                    StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
                    if (statsDisplay != null)
                    {
                        statsDisplay.UpdateStatsUI();
                    }

                    Debug.Log($"No more followers! Happiness decreased to {PlayerStats.Happiness}");
                }
                else
                {
                    Debug.Log("No followers found to destroy.");
                }
            }
            else
            {
                Debug.Log("Player has money, followers remain.");
            }
        }
    }
}
