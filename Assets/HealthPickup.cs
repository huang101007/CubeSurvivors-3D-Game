using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Recovering health")]
    public int healAmount = 20; // 吃到愛心回復的血量

    private void OnTriggerEnter(Collider other)
    {
        // 檢查碰到愛心的是否為玩家
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // 執行玩家身上的 Heal 函數
                playerHealth.Heal(healAmount);

                // 吃掉後銷毀愛心物件
                Destroy(gameObject);
            }
        }
    }
}