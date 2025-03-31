using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý Scene

public class HappyCheckObject : MonoBehaviour
{
    public int happyThreshold = 50;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu người chơi chạm vào Object
        if (collision.CompareTag("Player"))
        {
            // Kiểm tra chỉ số Happy
            if (PlayerStats.Happiness < happyThreshold)
            {
                // Reset lại level hiện tại
                Debug.Log("Happy is below threshold. Restarting level...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                // Chuyển sang Scene tiếp theo
                Debug.Log("Happy is above threshold. Moving to the next level...");
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

                // Kiểm tra nếu scene tiếp theo tồn tại
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    Debug.Log("No more levels! End of the game.");
                }
            }
        }
    }
}
