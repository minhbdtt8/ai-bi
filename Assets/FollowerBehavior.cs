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

    public TextMeshProUGUI narrativeText; // UI text for narrative messages

    public bool isActivated = false;
    private bool hasInteracted = false; // Ensure the interaction only happens once
    private bool isChildFollower = false; // Used for child followers

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

    // Called when spawning a child from prefab
    public void Initialize(Transform targetPlayer, TextMeshProUGUI narrativeUI, bool isChild = false)
    {
        player = targetPlayer;
        narrativeText = narrativeUI;
        isChildFollower = isChild;

        isActivated = true;

        if (isChildFollower)
        {
            ShowNarrative("A small soul has appeared – the embodiment of your journey.");
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
                PlayerStats.HasWife = true; // Register wife/follower
                

                ShowNarrative("A new companion has come to trust you and decided to walk alongside you.");
            }
            else
            {
                PlayerStats.Happiness = Mathf.Max(PlayerStats.Happiness - happinessPenalty, 0);

                StatsDisplay statsDisplay = FindObjectOfType<StatsDisplay>();
                if (statsDisplay != null)
                {
                    statsDisplay.UpdateStatsUI();
                }

                ShowNarrative("That person doesn’t trust you enough to follow. You feel a little down.");
                
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
