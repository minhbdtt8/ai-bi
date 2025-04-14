using UnityEngine;
using TMPro;

public class DecisionPoint : MonoBehaviour
{
    public Transform position1; // Vị trí cho selfishness < 33%
    public Transform position2; // Vị trí cho 33% <= selfishness <= 67%
    public Transform position3; // Vị trí cho selfishness > 67%

    public TextMeshProUGUI narrativeText; // Gắn Narrative Text UI vào đây

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int selfishness = PlayerStats.Selfishness;

            Transform nextPosition;
            string message = "";

            if (selfishness <= 33)
            {
                nextPosition = position1;
                message = "Bạn luôn quan tâm đến người khác. Con đường này mang theo hy vọng và lòng vị tha.";
            }
            else if (selfishness <= 67)
            {
                nextPosition = position2;
                message = "Bạn đang đấu tranh giữa lợi ích bản thân và người khác. Con đường này đầy những lựa chọn khó khăn.";
            }
            else
            {
                nextPosition = position3;
                message = "Bạn đặt bản thân lên trên hết. Con đường này cô đơn nhưng đầy quyền lực.";
            }

            // Di chuyển người chơi
            collision.transform.position = nextPosition.position;

            // Hiển thị đoạn văn kể chuyện
            if (narrativeText != null)
            {
                narrativeText.text = message;
            }

            Debug.Log("Story shown: " + message);
        }
    }
}
