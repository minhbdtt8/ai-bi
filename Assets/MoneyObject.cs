using UnityEngine;
using TMPro;

public class MoneyObject : MonoBehaviour
{
    public int minMoney = 5;
    public int maxMoney = 20;
    public float chanceToEarn = 0.5f;
    public float chanceToLose = 0.2f; // Tỉ lệ mất tiền
    public int selfishIncreaseAmount = 5;
    public int happinessIncreaseAmount = 3;
    public int happinessPenaltyAmount = 5; // Trừ Happiness khi mất tiền

    public TextMeshProUGUI narrativeText;

    private string[] lossNarratives = new string[]
    {
        "Bạn phải đóng tiền nhà, hơi buồn một chút.",
        "Bạn bị công an phạt vì đi sai luật.",
        "Bạn lỡ làm rơi ví trên đường.",
        "Một khoản phí bất ngờ đến làm bạn mất tiền.",
        "Bạn chi tiêu quá tay cho một món đồ không cần thiết."
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float roll = Random.value;

            if (roll <= chanceToEarn)
            {
                int earnedMoney = Random.Range(minMoney, maxMoney + 1);
                PlayerMoney.Instance.AddMoney(earnedMoney);

                PlayerStats.Selfishness += selfishIncreaseAmount;
                PlayerStats.Happiness = Mathf.Min(PlayerStats.Happiness + happinessIncreaseAmount, 100);

                ShowNarrative("Bạn đang cảm thấy hạnh phúc khi kiếm được tiền.");
                Debug.Log($"Player earned {earnedMoney} money! Selfishness: {PlayerStats.Selfishness}, Happiness: {PlayerStats.Happiness}");
            }
            else if (roll <= chanceToEarn + chanceToLose)
            {
                int lostMoney = Random.Range(minMoney, maxMoney + 1);
                PlayerMoney.Instance.AddMoney(-lostMoney);

                PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenaltyAmount, 0);

                string randomLossText = lossNarratives[Random.Range(0, lossNarratives.Length)];
                ShowNarrative(randomLossText);

                Debug.Log($"Player lost {lostMoney} money! Happiness: {PlayerStats.Happiness}");
            }
            else
            {
                ShowNarrative("Hôm nay bạn không nhận được gì cả.");
                Debug.Log("No money this time!");
            }

            // Cập nhật UI
            StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
            if (statsDisplay != null)
            {
                statsDisplay.UpdateStatsUI();
            }

            Destroy(gameObject);
        }
    }

    private void ShowNarrative(string message)
    {
        if (narrativeText != null)
        {
            narrativeText.text = message;
            CancelInvoke(nameof(ClearNarrative));
            Invoke(nameof(ClearNarrative), 5f);
        }
    }

    private void ClearNarrative()
    {
        if (narrativeText != null)
        {
            narrativeText.text = "";
        }
    }
}
