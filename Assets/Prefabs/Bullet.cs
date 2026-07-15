using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;

    // 接收來自玩家的傷害數值
    public int damage;

    [Header("Effects")]
    public GameObject hitEffectPrefab; // 擊中時的火花特效預製體

    void Start() { Destroy(gameObject, lifetime); }

    public void SetDirection(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 這裡的 speed 稍後會由 PlayerShooting 動態賦予
            rb.linearVelocity = direction * speed;
        }
    }

    // 改回觸發器偵測 (OnTriggerEnter)
    private void OnTriggerEnter(Collider other)
    {
        // 判斷是否打到敵人
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            SpawnHitEffect();
            Destroy(gameObject); // 銷毀子彈自身
        }
        // 判斷是否打到障礙物
        else if (other.CompareTag("Obstacle"))
        {
            SpawnHitEffect();
            Destroy(gameObject); // 銷毀子彈自身
        }
    }

    // 將生成特效的邏輯獨立成一個 Method，保持程式碼整潔
    private void SpawnHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            // 在子彈當前位置生成特效
            GameObject spark = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            // 1 秒後自動銷毀火花特效
            Destroy(spark, 0.3f);
        }
    }
}