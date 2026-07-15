using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public float spawnRadius = 12f;

    [Header("Spawn Speed (Difficulty Curve)")]
    public float currentSpawnInterval = 2f;    // 初始生成間隔
    public float minSpawnInterval = 0.5f;      // 最快生成間隔
    public float accelerationRate = 0.01f;     // 每秒減少的間隔時間
    private float timer = 0f;

    [Header("Boss Cycle")]
    public GameObject bossPrefab;
    public float bossInterval = 60f;           // 每隔幾秒出一次 Boss
    private float nextBossTime;                // 下一次出 Boss 的時間

    [Header("Phase Unlock Settings")]
    public float rangedUnlockTime = 30f;       // 遊戲開始幾秒後解鎖遠程方塊 (例如 30 秒)

    void Start()
    {
        nextBossTime = bossInterval;
    }

    void Update()
    {
        // 1. 難度隨時間漸增 (縮短生成間隔)
        if (currentSpawnInterval > minSpawnInterval)
        {
            currentSpawnInterval -= accelerationRate * Time.deltaTime;
        }

        // 2. 處理普通敵人生成
        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        // 3. 處理 Boss 定期降臨
        if (Time.timeSinceLevelLoad >= nextBossTime)
        {
            SpawnBoss();
            nextBossTime += bossInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        // 預設只能生成近戰怪 (Element 0)
        int maxIndex = 1;

        //如果當前遊戲時間超過了設定的解鎖時間，就開放抽到遠程怪 (Element 1)
        if (Time.timeSinceLevelLoad >= rangedUnlockTime)
        {
            maxIndex = enemyPrefabs.Length;
        }

        int randomIndex = Random.Range(0, maxIndex);

        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0.5f, randomCircle.y);
        Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }

    void SpawnBoss()
    {
        if (bossPrefab == null) return;

        Vector2 randomCircle = Random.insideUnitCircle.normalized * (spawnRadius * 1.5f);
        Vector3 spawnPosition = new Vector3(randomCircle.x, 1.5f, randomCircle.y);

        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        // 輸出內容已改為英文
        Debug.Log("Warning: BOSS Spawned!");
    }
}