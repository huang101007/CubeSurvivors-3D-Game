using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 移動速度，可以在 Unity 的 Inspector 中隨時調整
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        // 遊戲開始時，程式會自動抓取玩家身上的 Rigidbody 元件
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 接收玩家的 WASD 或方向鍵輸入 (數值範圍會自動在 -1 到 1 之間變化)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // 將輸入轉換為方向向量，並使用 normalized 標準化 (避免玩家同時按右上時，斜向移動速度過快)
        movement = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void FixedUpdate()
    {
        // 在物理更新幀中，根據方向與速度實際改變玩家的位置
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void UpgradeMoveSpeed(float amount)
    {
        // 假設你的移動速度變數叫 moveSpeed，每次升級加 1.5f
        moveSpeed += amount;
        Debug.Log("目前玩家移動速度提升為: " + moveSpeed);
    }
}