using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Generate settings")]
    public GameObject heartPrefab; // 愛心預製體
    public float spawnInterval = 10f; // 每隔幾秒生成一次
    public int maxHeartsOnField = 3; // 場上最多允許存在的愛心數量

    [Header("Generation range")]
    public float minX = -17f;
    public float maxX = 17f;
    public float minZ = -17f;
    public float maxZ = 17f;
    public float spawnY = 0.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // 當時間到達設定的間隔 (例如 10 秒)
        if (timer >= spawnInterval)
        {
            // 利用 Tag 尋找場上所有的愛心，並計算陣列長度 (數量)
            int currentHearts = GameObject.FindGameObjectsWithTag("Heart").Length;

            // 只有在當前數量小於上限時，才執行生成
            if (currentHearts < maxHeartsOnField)
            {
                SpawnItem();
            }

            // 不管剛才有沒有生成愛心，時間都重新計算
            timer = 0f;
        }
    }

    void SpawnItem()
    {
        if (heartPrefab != null)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);
            Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
        }
    }
}