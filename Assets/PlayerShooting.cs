using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float fireTimer = 0f;
    public int bulletDamage = 10;
    public float bulletSpeed = 15f;         // 子彈初始移動速度
    public float bulletSizeMultiplier = 1f; // 子彈大小倍率 (1 為原始大小)


    // 新增：多重射擊的核心變數
    public int projectileCount = 1;  // 每次發射的子彈數量
    public float spreadAngle = 45f;  // 扇形擴散的總角度

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            ShootAtNearestEnemy();
            fireTimer = 0f;
        }
    }

    void ShootAtNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            // 計算基礎的射擊方向 (直指敵人)
            Vector3 baseDirection = (closestEnemy.transform.position - transform.position).normalized;
            baseDirection.y = 0;

            // 利用 for 迴圈，根據 projectileCount 決定發射幾顆子彈
            for (int i = 0; i < projectileCount; i++)
            {
                Vector3 spawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
                GameObject bulletObj = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                // 動態改變子彈在 3D 空間中的縮放大小
                bulletObj.transform.localScale = Vector3.one * bulletSizeMultiplier;
                Bullet bullet = bulletObj.GetComponent<Bullet>();

                if (bullet != null)
                {
                    bullet.damage = bulletDamage;
                    bullet.speed = bulletSpeed;
                    // 計算每顆子彈的偏移角度
                    float currentAngle = 0f;
                    if (projectileCount > 1)
                    {
                        // 算出扇形的起始角度，以及每顆子彈之間的間隔角度
                        float startAngle = -spreadAngle / 2f;
                        float angleStep = spreadAngle / (projectileCount - 1);
                        currentAngle = startAngle + (angleStep * i);
                    }

                    // 將基礎方向沿著 Y 軸旋轉 currentAngle 的角度
                    Vector3 finalDirection = Quaternion.Euler(0, currentAngle, 0) * baseDirection;
                    bullet.SetDirection(finalDirection);
                }
            }
        }
    }

    // ====== 給升級選單呼叫的強化接口 ======
    public void UpgradeFireRate(float multiplier)
    {
        fireRate *= multiplier;
    }

    public void UpgradeDamage(int amount)
    {
        bulletDamage += amount;
    }

    // 新增：增加子彈數量的接口
    public void UpgradeProjectileCount(int amount)
    {
        projectileCount += amount;
        Debug.Log("目前發射子彈數量提升為: " + projectileCount);
    }

    // 給升級系統呼叫：增加子彈大小
    public void UpgradeBulletSize(float amount)
    {
        bulletSizeMultiplier += amount;
        Debug.Log("目前子彈大小提升為: " + bulletSizeMultiplier);
    }

    // 給升級系統呼叫：增加子彈速度
    public void UpgradeBulletSpeed(float amount)
    {
        bulletSpeed += amount;
        Debug.Log("目前子彈速度提升為: " + bulletSpeed);
    }

}