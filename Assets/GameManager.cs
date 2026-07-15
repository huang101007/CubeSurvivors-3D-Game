using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Stats")]
    public int currentScore = 0;
    public bool isFirstBossDefeated = false;

    [Header("Level Goals")]
    public float survivalTime = 60f; // 通關所需時間 (測試時可先設為 60 秒)
    private float currentTime;
    private bool isGameEnded = false; // 判斷遊戲是否已經結束 (勝利或失敗)

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText; // 倒數計時文字
    public GameObject gameOverPanel;
    public GameObject victoryPanel;   //勝利畫面面板 (預留給下一階段)

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // 遊戲開始時，將目前時間設為目標生存時間
        currentTime = survivalTime;
        Time.timeScale = 1f;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }

    void Update()
    {
        // 如果遊戲已結束，停止計時
        if (isGameEnded) return;

        // 倒數計時邏輯
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            // 當時間歸零，觸發勝利
            if (currentTime <= 0)
            {
                currentTime = 0;
                UpdateTimerUI();
                TriggerVictory();
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // 將秒數轉換為 MM:SS 格式
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            timerText.text = "Time left\n"+string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void AddScore(int amount)
    {
        if (isGameEnded) return;
        currentScore += amount;
        if (scoreText != null) scoreText.text = "SCORE: " + currentScore;
    }

    public void NotifyBossDefeated()
    {
        if (!isFirstBossDefeated)
        {
            isFirstBossDefeated = true;
            Debug.Log("First Boss Defeated! Unlocking Ranged Enemies!");
        }
    }

    public void GameOver()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Debug.Log("Game Over!");
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 觸發勝利的函數
    public void TriggerVictory()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Debug.Log("Victory!");
        if (victoryPanel != null) victoryPanel.SetActive(true);
        Time.timeScale = 0f; // 暫停遊戲
    }

    // 重玩本關 (綁定給按鈕用)
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 返回主畫面 (綁定給按鈕用)
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}