using UnityEngine;
using TMPro;

public class DecisionPoint : MonoBehaviour
{
    public Transform position1; // Position for selfishness < 33%
    public Transform position2; // Position for 33% <= selfishness <= 67%
    public Transform position3; // Position for selfishness > 67%

    public TextMeshProUGUI narrativeText; // Assign the Narrative Text UI here

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
                message = "You always care about others. This path is filled with hope and compassion.";
            }
            else if (selfishness <= 67)
            {
                nextPosition = position2;
                message = "You are torn between self-interest and concern for others. This path is full of difficult choices.";
            }
            else
            {
                nextPosition = position3;
                message = "You prioritize yourself above all. This path is lonely, but full of power.";
            }

            // Move the player
            collision.transform.position = nextPosition.position;

            // Show the narrative message
            if (narrativeText != null)
            {
                narrativeText.text = message;
            }

            
        }
    }
}
