using UnityEngine;
using UnityEngine.UI; // 必須引入此行才能控制 Slider

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider; // 用來綁定 UI 血條的欄位

    void Start()
    {
        currentHealth = maxHealth;

        // 遊戲開始時，確保血條 UI 顯示滿血
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // 更新 UI 血條的顯示數值
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            // 呼叫 GameManager 執行遊戲結束邏輯
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            // 銷毀玩家物件
            Destroy(gameObject);
        }
    }
    // 給升級按鈕呼叫的回血功能
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // 防止血量補超過上限
        }

        // 更新 UI
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
}