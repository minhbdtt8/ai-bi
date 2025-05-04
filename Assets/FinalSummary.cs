using UnityEngine;
using TMPro;

public class FinalSummary : MonoBehaviour
{
    public TextMeshProUGUI summaryText;      // Text to show the detailed life story
    public TextMeshProUGUI endingTypeText;   // Text to show the type of ending (Good, Bad, Nature, Accident)
    public float autoQuitDelay = 30f;        // Time before quitting the application

    void Start()
    {
        ShowSummary();
        Invoke(nameof(QuitApplication), autoQuitDelay);
    }

    void ShowSummary()
    {
        int happiness = PlayerStats.Happiness;
        int diligence = PlayerStats.Diligence;
        int selfishness = PlayerStats.Selfishness;
        int money = PlayerMoney.Instance.money;

        bool hasWife = PlayerStats.HasWife;
        bool diedInAccident = PlayerStats.IsDeadByAccident;

        bool hasChildren = hasWife && Random.value > 0.3f;
        int numberOfChildren = hasChildren ? Random.Range(1, 4) : 0;

        string story = $"📊 Your Life Summary:\n\n" +
                       $"- Happiness: {happiness}\n" +
                       $"- Diligence: {diligence}\n" +
                       $"- Selfishness: {selfishness}\n" +
                       $"- Money: {money}\n" +
                       $"- Married: {(hasWife ? "Yes" : "No")}\n" +
                       $"- Children: {(hasChildren ? numberOfChildren.ToString() : "None")}\n\n";

        // ========== ACCIDENT ENDING ==========
        if (diedInAccident)
        {
            story += "💥 Your life was tragically cut short by an accident.\n";

            if (happiness >= 60)
                story += "😔 You left behind a life full of potential and joy.\n";
            else if (money >= 80)
                story += "🏦 Despite all your wealth, it couldn’t save you from fate.\n";
            else
                story += "🕯 You were gone before you could truly make your mark.\n";

            story += "🖤 A tragic ending, reminding us how unpredictable life can be.";
            endingTypeText.text = "Accident Ending";
        }

        // ========== BAD ENDING ==========
        else if ((money < 0 || selfishness > 80 || happiness < 30 || diligence < 30) && !(hasWife && happiness >= 60))
        {
            if (money < 0)
            {
                story += "💸 Your life took a turn for the worse as debt piled up.\n";
                story += "🔴 Despite your best efforts, you couldn't escape the financial troubles.\n";
                story += "😔 You ended up bankrupt, with no way to pay your debts.\n";
            }
            else
            {
                story += "💔 Your selfishness took over, and you abandoned meaningful relationships.\n";
                story += "😞 Happiness was hard to find as you only focused on your own desires.\n";
                story += "🔻 Your lack of effort and care left you with little to cherish.\n";
            }

            story += "🖤 A truly unfortunate ending.";
            endingTypeText.text = "Bad Ending";
        }

        // ========== GOOD ENDING ==========
        else if (happiness >= 70 && hasWife)
        {
            story += "✨ You lived a fulfilling life with your beloved partner. Travel, laughter, and shared dreams made life beautiful.\n";
            if (hasChildren)
                story += $"👨‍👩‍👧‍👦 You raised {numberOfChildren} wonderful children, and each day was a new adventure with them.\n";

            story += "🌈 This is a truly happy ending.";
            endingTypeText.text = "Good Ending";
        }

        // ========== NEUTRAL ENDING ==========
        else
        {
            story += "🌤 You lived a modest life — not too grand, not too hard — and you found moments to smile.\n";
            story += "🙂 A quiet, peaceful ending.";
            endingTypeText.text = "Neutral Ending";
        }

        summaryText.text = story;
    }


    // Hàm này sẽ thoát ứng dụng sau 30 giây
    void QuitApplication()
    {
        
        Application.Quit();

        // Nếu đang trong chế độ Editor, dừng ứng dụng (thử nghiệm)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
