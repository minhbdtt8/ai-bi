using UnityEngine;

public class CheckMoneyObject : MonoBehaviour
{
    public string followerTag = "Follower"; // Tag để nhận diện tất cả các Follower Object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu người chơi chạm vào CheckMoneyObject
        if (collision.CompareTag("Player"))
        {
            // Kiểm tra số tiền của người chơi
            if (PlayerMoney.Instance.money <= 0)
            {
                // Tìm tất cả các Follower Object trong scene bằng tag
                GameObject[] followers = GameObject.FindGameObjectsWithTag(followerTag);

                foreach (GameObject follower in followers)
                {
                    // Hủy từng Follower Object
                    Destroy(follower);
                    Debug.Log("Destroyed follower: " + follower.name);
                }
            }
            else
            {
                Debug.Log("Player has money, followers remain.");
            }
        }
    }
}
