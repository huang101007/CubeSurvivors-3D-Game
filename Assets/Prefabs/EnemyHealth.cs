using UnityEngine;
using UnityEngine.UI; // Required for Slider UI

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public int scoreValue = 10;

    public bool isBoss = false;
    public GameObject expGemPrefab;

    [Header("Effects")]
    public GameObject deathEffectPrefab; // 宣告死亡特效的預製體參考

    [Header("UI Settings")]
    public Slider healthBar; // Reference to the health bar UI

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar if it exists
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar UI
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(scoreValue);

                if (isBoss)
                {
                    GameManager.instance.NotifyBossDefeated();
                }
            }

            if (expGemPrefab != null)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
                Instantiate(expGemPrefab, spawnPos, Quaternion.identity);
            }

            // 觸發死亡爆炸特效
            if (deathEffectPrefab != null)
            {
                // 1. 生成特效，並將它暫時存入一個名為 effect 的變數中
                GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

                // 2. 告訴系統：請在 2f (2秒) 後，銷毀這個 effect 物件
                Destroy(effect, 1f);
            }

            Destroy(gameObject);
        }
    }
}