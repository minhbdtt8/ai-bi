using UnityEngine;
using TMPro;

public class FollowerBehavior : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;
    public float safeDistance = 1.5f;
    public int minDiligence = 0;
    public int maxDiligence = 30;
    public int happinessPenalty = 20;
    
    public TextMeshProUGUI narrativeText; // Text UI để kể chuyện

    public bool isActivated = false;
    private bool hasInteracted = false; // Đảm bảo chỉ kích hoạt 1 lần
    private bool isChildFollower = false; // Dùng cho follower là con

    private void Update()
    {
        if (isActivated && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > safeDistance)
            {
                Vector3 targetPosition = player.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }
        }
    }

    // Dùng khi tạo "con" từ prefab
    public void Initialize(Transform targetPlayer, TextMeshProUGUI narrativeUI, bool isChild = false)
    {
        player = targetPlayer;
        narrativeText = narrativeUI;
        isChildFollower = isChild;

        isActivated = true;

        if (isChildFollower)
        {
            ShowNarrative("Một sinh linh bé nhỏ xuất hiện – là kết tinh từ hành trình của bạn.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasInteracted)
        {
            hasInteracted = true;

            if (IsWithinDiligenceRange())
            {
                isActivated = true;
                PlayerStats.HasWife = true; // <- thêm dòng này
                Debug.Log("Object follows player!");

                ShowNarrative("Một người bạn mới tin tưởng bạn và quyết định đồng hành cùng bạn.");
            }

            else
            {
                PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenalty, 0);

                StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStatsUI();
                }

                ShowNarrative("Người đó không tin bạn đủ để đi cùng. Bạn cảm thấy hơi buồn.");
                Debug.Log($"Diligence too low! Happiness decreased to {PlayerStats.Happiness}");
            }
        }
    }

    private bool IsWithinDiligenceRange()
    {
        return PlayerStats.Diligence >= minDiligence && PlayerStats.Diligence <= maxDiligence;
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

