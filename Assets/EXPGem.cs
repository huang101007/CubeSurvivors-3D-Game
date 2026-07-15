using UnityEngine;

public class EXPGem : MonoBehaviour
{
    public int expValue = 10; // 這顆寶石提供多少經驗值

    // 讓寶石有一點上下浮動的動畫效果，看起來更生動
    private float floatSpeed = 2f;
    private float floatHeight = 0.2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 簡單的正弦波浮動效果
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果是玩家碰到了這顆寶石
        if (other.CompareTag("Player"))
        {
            // 呼叫玩家身上的升級系統 (我們稍後會寫)
            PlayerLevel playerLevel = other.GetComponent<PlayerLevel>();
            if (playerLevel != null)
            {
                playerLevel.AddExperience(expValue);
            }

            // 玩家吸收到經驗後，銷毀這顆寶石
            Destroy(gameObject);
        }
    }
}