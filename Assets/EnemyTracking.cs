using UnityEngine;
using UnityEngine.AI; // 必須引入 AI 函式庫

public class EnemyTracking : MonoBehaviour
{
    public int attackDamage = 10;

    private Transform playerTransform;
    private NavMeshAgent agent; // 宣告導航代理器

    void Start()
    {
        // 抓取身上的 NavMesh Agent
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // 只要玩家還活著，就將目的地設定為玩家的位置。AI 會自動計算繞過障礙物的最佳路徑！
        if (playerTransform != null)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    // 傷害判定邏輯保持不變
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}