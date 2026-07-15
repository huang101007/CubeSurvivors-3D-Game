using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // 要跟隨的目標 (玩家)

    [Header("Camera Dynamics")]
    public Vector3 offset = new Vector3(0, 12f, -8f); // 攝影機與玩家的相對距離 (可與你剛剛設定的 Position 一致)
    public float smoothSpeed = 5f; // 平滑跟隨的速度 (數值越低越平滑，越高越緊跟)

    void LateUpdate()
    {
        // 防呆機制：如果玩家死了或還沒綁定，就不執行跟隨
        if (target == null) return;

        // 計算攝影機「應該要到達的理想位置」(玩家目前位置 + 偏移值)
        Vector3 desiredPosition = target.position + offset;

        // 使用 Vector3.Lerp 讓目前的攝影機位置平滑過渡到理想位置
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 更新攝影機的實際位置
        transform.position = smoothedPosition;
    }
}