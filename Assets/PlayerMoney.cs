using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance; // Để truy cập dễ dàng từ các script khác

    public int money = 0; // Số tiền hiện tại của người chơi
    public Text moneyText; // Text UI để hiển thị tiền

    private void Awake()
    {
        // Thiết lập singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateMoneyUI();
    }

    // Hàm để thêm tiền
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    // Cập nhật tiền trên UI
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }
}

