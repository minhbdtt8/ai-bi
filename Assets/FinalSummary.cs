using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalSummary : MonoBehaviour
{
    public TextMeshProUGUI summaryText;
    public string sceneToLoadAfter = "MainMenu";

    void Start()
    {
        ShowSummary();
    }

    void ShowSummary()
    {
        int happiness = PlayerStats.Happiness;
        int diligence = PlayerStats.Diligence;
        int selfishness = PlayerStats.Selfishness;
        int money = PlayerMoney.Instance.money;

        bool hasWife = PlayerStats.HasWife;



        bool hasChildren = hasWife && Random.value > 0.3f;
        int numberOfChildren = hasChildren ? Random.Range(1, 4) : 0;

        string story = $"📊 Tổng kết cuộc đời bạn:\n\n" +
                       $"- Hạnh phúc: {happiness}\n" +
                       $"- Siêng năng: {diligence}\n" +
                       $"- Ích kỷ: {selfishness}\n" +
                       $"- Tiền tích lũy: {money}\n" +
                       $"- Vợ: {(hasWife ? "Có" : "Không")}\n" +
                       $"- Con: {(hasChildren ? numberOfChildren.ToString() : "Không có")}\n\n";

        // Viết truyện dựa vào stats
        if (happiness >= 70 && hasWife)
        {
            story += "✨ Bạn đã sống một cuộc đời viên mãn bên người bạn đời của mình. Những chuyến du lịch, những nụ cười bên nhau làm cuộc sống trở nên tươi đẹp hơn.\n";
            if (hasChildren)
            {
                story += $"👨‍👩‍👧‍👦 Gia đình bạn có {numberOfChildren} đứa trẻ đáng yêu, và mỗi ngày là một hành trình tuyệt vời cùng chúng.\n";
            }
            story += "🌈 Đây chắc chắn là một happy ending đáng nhớ.";
        }
        else if (money >= 80 && selfishness >= 60)
        {
            story += "💼 Bạn đã trở nên cực kỳ giàu có, nhưng vì theo đuổi tiền tài, bạn đánh mất nhiều mối quan hệ quan trọng.\n";
            story += "😶 Dù thành công, bạn vẫn thường cô đơn trong căn biệt thự rộng lớn.\n";
            story += "🖤 Một kết thúc có lẽ là good, nhưng không hẳn là happy.";
        }
        else if (diligence < 30 && happiness < 40)
        {
            story += "🥀 Cuộc đời bạn trải qua nhiều vấp ngã, có lẽ do thiếu nỗ lực và tinh thần vượt khó.\n";
            story += "🧍 Bạn sống một mình trong căn phòng nhỏ, nghĩ về những điều lẽ ra có thể khác đi.\n";
            story += "😭 Một kết thúc buồn – nhưng biết đâu là khởi đầu cho một chương mới nếu có cơ hội...";
        }
        else
        {
            story += "🌤 Bạn có một cuộc sống bình dị, không quá thành công nhưng đủ để bạn mỉm cười mỗi khi nhìn lại.\n";
            story += "🙂 Một cái kết nhẹ nhàng, đủ đầy.";
        }

        summaryText.text = story;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneToLoadAfter);
    }
}
