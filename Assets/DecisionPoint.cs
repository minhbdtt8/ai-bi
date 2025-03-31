using UnityEngine;

public class DecisionPoint : MonoBehaviour
{
    public Transform position1; // V? tr? cho selfishness < 33%
    public Transform position2; // V? tr? cho 33% <= selfishness <= 67%
    public Transform position3; // V? tr? cho selfishness > 67%

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // L?y ch? s? selfishness t? PlayerStats
            int selfishness = PlayerStats.Selfishness;

            // Quy?t ??nh v? tr? ti?p theo d?a v?o selfishness
            Transform nextPosition;
            if (selfishness <= 33)
                nextPosition = position1;
            else if (selfishness <= 67)
                nextPosition = position2;
            else
                nextPosition = position3;

            // Di chuy?n nh?n v?t t?i v? tr? ti?p theo
            collision.transform.position = nextPosition.position;
        }
    }
}

