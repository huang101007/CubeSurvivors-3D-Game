using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab; // 用來放敵人專屬子彈
    public float fireRate = 1.5f;        // 每隔幾秒開一槍
    public float shootingRange = 10f;    // 進入此距離才會開槍

    private float fireTimer = 0f;
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // 如果玩家死了，就停止運作
        if (playerTransform == null) return;

        // 計時器累加
        fireTimer += Time.deltaTime;

        // 隨時計算自己與玩家的距離
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 如果距離夠近，且冷卻時間到了，就開槍！
        if (distanceToPlayer <= shootingRange && fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f; // 重置計時器
        }
    }

    void Shoot()
    {
        if (playerTransform == null) return;

        Vector3 playerPos = playerTransform.position;
        Vector3 bossPos = transform.position;

        // 1. 先精準算出朝向玩家的方向向量
        Vector3 shootDirection = new Vector3(playerPos.x - bossPos.x, 0f, playerPos.z - bossPos.z).normalized;

        // 2. 【核心修正】：將生成位置往射擊方向往前推一段距離 (例如 2.5 單位)，避免生在 Boss 肚子裡
        Vector3 spawnPosition = bossPos + (shootDirection * 2.5f);
        spawnPosition.y = 0.5f; // 保持子彈的高度不變

        // 3. 生成子彈
        GameObject bulletObj = Instantiate(enemyBulletPrefab, spawnPosition, Quaternion.identity);

        EnemyBullet bulletScript = bulletObj.GetComponent<EnemyBullet>();
        if (bulletScript != null)
        {
            // 將方向賦予子彈
            bulletScript.SetDirection(shootDirection);
        }
    }
}