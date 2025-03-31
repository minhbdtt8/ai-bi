using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 2f;

    void Update()
    {
        // Nhân vật tự di chuyển sang phải
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
