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
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                // Chuyển sang Scene tiếp theo
               
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

                // Kiểm tra nếu scene tiếp theo tồn tại
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    
                }
            }
        }
    }
}
