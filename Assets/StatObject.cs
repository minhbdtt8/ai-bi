using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatObject : MonoBehaviour
{
    public enum StatType { Happiness, Diligence, Selfishness }

    public StatType statType;
    public int minValue = 5;
    public int maxValue = 20;
    public float chanceToGain = 0.6f;
    public float chanceToLose = 0.3f;
    private float chanceOfDeath = 0.05f;

    public int deathHappinessLoss = 100; // if player dies in accident
    public TextMeshProUGUI narrativeText;

    private string[] accidentNarratives = new string[]
    {
        "You suffered a serious accident while working too hard...",
        "An unexpected incident took your life suddenly...",
        "You slipped and fell while rushing...",
        "Your life ended abruptly due to a tragic accident..."
    };

    private string[] gainNarratives = new string[]
    {
        "You felt a positive boost today!",
        "Something happened that made you more confident.",
        "You're doing well and feeling motivated.",
        "A good deed made you proud of yourself."
    };

    private string[] loseNarratives = new string[]
    {
        "You lacked motivation today.",
        "You did something you regret.",
        "Things didn’t go as planned...",
        "You lost control and feel disappointed."
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float roll = Random.value;

            if (roll <= chanceOfDeath)
            {
                // Death by accident
                PlayerStats.IsDeadByAccident = true;
                PlayerStats.Happiness = Mathf.Max(0, PlayerStats.Happiness - deathHappinessLoss);

                ShowNarrative(accidentNarratives[Random.Range(0, accidentNarratives.Length)]);
                SceneManager.LoadScene("End");
            }
            else if (roll <= chanceOfDeath + chanceToGain)
            {
                int gain = Random.Range(minValue, maxValue + 1);
                ModifyStat(gain);
                ShowNarrative(gainNarratives[Random.Range(0, gainNarratives.Length)]);
            }
            else
            {
                int loss = Random.Range(minValue, maxValue + 1);
                ModifyStat(-loss);
                ShowNarrative(loseNarratives[Random.Range(0, loseNarratives.Length)]);
            }

            StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
            if (statsDisplay != null)
                statsDisplay.UpdateStatsUI();

            Destroy(gameObject);
        }
    }

    void ModifyStat(int amount)
    {
        switch (statType)
        {
            case StatType.Happiness:
                PlayerStats.Happiness = Mathf.Clamp(PlayerStats.Happiness + amount, 0, 100);
                break;
            case StatType.Diligence:
                PlayerStats.Diligence = Mathf.Clamp(PlayerStats.Diligence + amount, 0, 100);
                break;
            case StatType.Selfishness:
                PlayerStats.Selfishness = Mathf.Clamp(PlayerStats.Selfishness + amount, 0, 100);
                break;
        }
    }

    void ShowNarrative(string message)
    {
        if (narrativeText != null)
        {
            narrativeText.text = message;
            CancelInvoke(nameof(ClearNarrative));
            Invoke("ClearNarrative", 5f);
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
