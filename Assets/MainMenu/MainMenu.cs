using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject levelSelectPanel;

    void Start()
    {
        // 確保遊戲啟動時，只顯示主選單，隱藏關卡選擇畫面
        if (mainPanel != null) mainPanel.SetActive(true);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(false);
    }

    // 點擊 "LEVEL SELECT" 時呼叫：隱藏主畫面，顯示關卡畫面
    public void OpenLevelSelect()
    {
        if (mainPanel != null) mainPanel.SetActive(false);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(true);
    }

    // 點擊 "BACK" 時呼叫：隱藏關卡畫面，顯示主畫面
    public void CloseLevelSelect()
    {
        if (levelSelectPanel != null) levelSelectPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    // 點擊 "SKILLS" 時呼叫 (暫時的空功能)
    public void OpenSkills()
    {
        Debug.Log("Skills Menu is under construction!");
    }

    // 點擊具體的關卡按鈕時呼叫 (利用 string 動態傳入場景名稱)
    public void StartSpecificLevel(string levelName)
    {
        Debug.Log("Loading Level: " + levelName);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }
}