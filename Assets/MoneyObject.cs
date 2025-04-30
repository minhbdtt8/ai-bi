using UnityEngine;
using TMPro;

public class MoneyObject : MonoBehaviour
{
    public int minMoney = 5;
    public int maxMoney = 20;
    public float chanceToEarn = 0.5f;
    public float chanceToLose = 0.2f;
    public int selfishIncreaseAmount = 5;
    public int happinessIncreaseAmount = 3;
    public int happinessPenaltyAmount = 5;

    public TextMeshProUGUI narrativeText;

    private string[] earnNarratives = new string[]
    {
        "💰 You found an unexpected bonus today!",
        "🎉 A small freelance gig brought in some cash.",
        "😊 Your efforts paid off with extra money.",
        "📈 You received a surprise reward from work!"
    };

    private string[] lossNarratives = new string[]
    {
        "💸 Rent was due, and it left your wallet light.",
        "🚨 You were fined for breaking a minor law.",
        "😓 You dropped your wallet on the street.",
        "💳 An unexpected charge drained your account.",
        "🛍️ You overspent on something you didn't need."
    };

    private string[] noChangeNarratives = new string[]
    {
        "🤷‍♂️ Nothing significant happened today.",
        "📉 A slow day—no money gained or lost.",
        "🕐 You waited all day but no opportunity came.",
        "🌧️ Just one of those unremarkable days."
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

                ShowNarrative(earnNarratives[Random.Range(0, earnNarratives.Length)]);
            }
            else if (roll <= chanceToEarn + chanceToLose)
            {
                int lostMoney = Random.Range(minMoney, maxMoney + 1);
                PlayerMoney.Instance.AddMoney(-lostMoney);

                PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenaltyAmount, 0);

                ShowNarrative(lossNarratives[Random.Range(0, lossNarratives.Length)]);
            }
            else
            {
                ShowNarrative(noChangeNarratives[Random.Range(0, noChangeNarratives.Length)]);
            }

            // Update UI
            StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
            if (statsDisplay != null)
                statsDisplay.UpdateStatsUI();

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
