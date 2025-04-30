using UnityEngine;
using TMPro;

public class CheckMoneyObject : MonoBehaviour
{
    public string followerTag = "Follower"; // Tag để nhận diện tất cả các Follower Object
    public int happinessPenalty = 40; // Lượng Happiness bị trừ nếu không còn follower
    public TextMeshProUGUI narrativeText; // Thêm biến hiển thị story

    private string[] lostFollowerStories = new string[]
    {
        "💔 She left because life became too hard without stability...",
        "😢 Without enough money, she couldn't stay by your side.",
        "🕊 Love faded when the struggles became too heavy to carry.",
        "🥀 You watched her walk away, taking your joy with her..."
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerMoney.Instance.money <= 45)
            {
                GameObject[] followers = GameObject.FindGameObjectsWithTag(followerTag);

                if (followers.Length > 0)
                {
                    foreach (GameObject follower in followers)
                    {
                        Destroy(follower);
                    }

                    // Trừ điểm Happiness vì mất follower
                    PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenalty, 0);

                    // Cập nhật trạng thái không còn vợ
                    PlayerStats.HasWife = false;

                    // Hiện narrative buồn
                    ShowNarrative(lostFollowerStories[Random.Range(0, lostFollowerStories.Length)]);

                    // Cập nhật UI nếu có
                    StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
                    if (statsDisplay != null)
                        statsDisplay.UpdateStatsUI();
                }
            }
        }
    }

    void ShowNarrative(string message)
    {
        if (narrativeText != null)
        {
            narrativeText.text = message;
            CancelInvoke(nameof(ClearNarrative));
            Invoke(nameof(ClearNarrative), 5f);
        }
    }

    void ClearNarrative()
    {
        if (narrativeText != null)
        {
            narrativeText.text = "";
        }
    }
}
