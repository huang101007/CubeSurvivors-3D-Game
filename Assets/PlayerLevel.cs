using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; // 為了使用 List 容器

public class PlayerLevel : MonoBehaviour
{
    // 1. 定義所有可能的升級技能庫 (Enum)
    public enum UpgradeType
    {
        Heal,
        FireRate,
        Damage,
        MultiShot,
        MoveSpeed,
        BulletSize,
        BulletSpeed
    }

    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 50;

    [Header("Level Up Settings")]
    public int healAmountOnLevelUp = 10;

    [Header("UI Experience Bar Component")]
    public Slider expSlider;
    public TextMeshProUGUI levelText;

    [Header("Upgrade menu components")]
    public GameObject levelUpPanel;
    public TextMeshProUGUI[] buttonTexts; // 陣列：用來放入 3 個按鈕的文字組件

    // 紀錄當前這三個按鈕各自代表什麼技能
    private UpgradeType[] activeUpgrades = new UpgradeType[3];

    void Start()
    {
        UpdateUI();
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    void LevelUp()
    {
        currentExp -= expToNextLevel;
        currentLevel++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.2f);

        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmountOnLevelUp);
        }

        // 觸發隨機抽選技能
        GenerateUpgradeOptions();

        if (levelUpPanel != null) levelUpPanel.SetActive(true);
        Time.timeScale = 0f; // 暫停遊戲
    }

    // 隨機抽選 3 個不重複技能的核心演算法
    void GenerateUpgradeOptions()
    {
        // 建立一個臨時清單，把所有 7 種技能放進去
        List<UpgradeType> allUpgrades = new List<UpgradeType>
        {
            UpgradeType.Heal, UpgradeType.FireRate, UpgradeType.Damage,
            UpgradeType.MultiShot, UpgradeType.MoveSpeed, UpgradeType.BulletSize,
            UpgradeType.BulletSpeed
        };

        // 迴圈跑 3 次，幫 3 個按鈕各自抽一個技能
        for (int i = 0; i < 3; i++)
        {
            // 隨機抽取一個索引值
            int randomIndex = Random.Range(0, allUpgrades.Count);
            UpgradeType selectedUpgrade = allUpgrades[randomIndex];

            // 紀錄該按鈕代表的技能
            activeUpgrades[i] = selectedUpgrade;

            // 根據抽到的技能，動態更改按鈕顯示的中文文字
            if (buttonTexts[i] != null)
            {
                buttonTexts[i].text = GetUpgradeDescription(selectedUpgrade);
            }

            //將抽到的技能從臨時清單中移除，確保下一輪絕對不會抽到重複的！
            allUpgrades.RemoveAt(randomIndex);
        }
    }

    // 根據 Enum 回傳對應文字的輔助函數
    string GetUpgradeDescription(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Heal: return "Medkit: \nRestores 50 health points";
            case UpgradeType.FireRate: return "Microchip: \n20% increase in firing rate";
            case UpgradeType.Damage: return "High-energy gunpowder: \nBullet damage +10";
            case UpgradeType.MultiShot: return "Double Strike: \nBullet count +1";
            case UpgradeType.MoveSpeed: return "Power Shoes: \nMovement Speed ​​+1.5";
            case UpgradeType.BulletSize: return "Expansion: \nThe bullet's volume increases.";
            case UpgradeType.BulletSpeed: return "Electromagnetic acceleration: \nbullets move faster";
            default: return "Unknown enhancement";
        }
    }

    // 點擊按鈕時的通用接收函數 (由 Inspector 傳入 0, 1, 或 2)
    public void OnSelectOption(int buttonIndex)
    {
        // 知道玩家點了哪一個按鈕後，去查該按鈕當時被賦予了什麼技能
        UpgradeType chosenUpgrade = activeUpgrades[buttonIndex];

        // 根據對應的技能，執行玩家身上相應腳本的接口
        switch (chosenUpgrade)
        {
            case UpgradeType.Heal:
                GetComponent<PlayerHealth>().Heal(50);
                break;
            case UpgradeType.FireRate:
                GetComponent<PlayerShooting>().UpgradeFireRate(0.8f); // 射擊間隔乘以 0.8 (變快)
                break;
            case UpgradeType.Damage:
                GetComponent<PlayerShooting>().UpgradeDamage(10);
                break;
            case UpgradeType.MultiShot:
                GetComponent<PlayerShooting>().UpgradeProjectileCount(1);
                break;
            case UpgradeType.MoveSpeed:
                // 這裡會尋找你掛在玩家身上的移動腳本，請確保名稱對齊
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().UpgradeMoveSpeed(1.5f);
                break;
            case UpgradeType.BulletSize:
                GetComponent<PlayerShooting>().UpgradeBulletSize(0.4f); // 每次體積變大 40%
                break;
            case UpgradeType.BulletSpeed:
                GetComponent<PlayerShooting>().UpgradeBulletSpeed(5f); // 速度加 5
                break;
        }

        ResumeGame();
    }

    void ResumeGame()
    {
        if (levelUpPanel != null) levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void UpdateUI()
    {
        if (expSlider != null) { expSlider.maxValue = expToNextLevel; expSlider.value = currentExp; }
        if (levelText != null) { levelText.text = "LV: " + currentLevel; }
    }
}