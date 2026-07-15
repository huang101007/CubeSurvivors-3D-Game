# CubeSurvivors-3D-Game
使用Unity開發的3D俯視角Roguelite生存遊戲。

## 遊戲簡介
在Cube Survivors中，玩家必須在封閉的競技場中無盡的敵人攻勢下生存。透過擊殺敵人，玩家可以收集EXP寶石來升級並獲取隨機的能力強化。撐到倒數計時結束即可獲得勝利！

## 遊戲畫面

**主畫面**

<img width="1278" height="710" alt="主畫面" src="https://github.com/user-attachments/assets/02bf061e-8f3f-4937-a09a-499f96cb6f52" />

---
**選擇關卡**

<img width="1277" height="708" alt="選擇關卡" src="https://github.com/user-attachments/assets/62a7b5a9-b82a-41e7-a5c6-168cb8dd8bea" />

---
**生存前期**
只有黃色近戰小兵

<img width="1271" height="706" alt="image" src="https://github.com/user-attachments/assets/e6583a1f-ab66-4f14-b990-e156c29bb49b" />

---
**生存中期**
開始出現紅色遠程小兵、隨機回血包

<img width="1272" height="702" alt="image" src="https://github.com/user-attachments/assets/4d12bc3b-21bf-44a2-9496-4324b61eac5c" />

---
**生存後期**
開始出現boss，會吐石頭攻擊，可依靠場上障礙物躲避，若擊敗會獲得大量經驗值

<img width="1266" height="703" alt="image" src="https://github.com/user-attachments/assets/38247b5b-f091-481d-b0c5-13018699eb93" />

---
**升級畫面**
有七種隨機技能可以選擇(Heal、Fire Rate、Damage、Multi-Shot、Move Speed、Bullet Speed與Bullet Size)

<img width="1266" height="698" alt="image" src="https://github.com/user-attachments/assets/a1f1839f-cbfc-466f-8f94-b69004a3ee88" />


## 核心特色
* 俯視角射擊機制：平滑的攝影機跟隨與自動瞄準系統。
* Roguelite升級系統：升級時隨機抽取技能(包含Heal、Fire Rate、Damage、Multi-Shot、Move Speed與Bullet Size)。
* 動態敵人生成：難度隨時間增加，包含近戰敵人、遠程敵人以及強大的Boss。
* 完整的遊戲迴圈：包含主畫面、關卡選擇、遊戲計時器以及勝利與遊戲結束結算畫面。

## 操作方式
* W, A, S, D：控制角色移動。
* 射擊：全自動射擊(自動鎖定最近的敵人)。
* UI導覽：滑鼠點擊。

## 技術規格
* 開發引擎：Unity 6
* 程式語言：C#
* 架構設計：物件導向程式設計(OOP)與基於組件的設計。
* 關鍵系統：UnityEngine.AI(NavMesh)、UnityEngine.SceneManagement、TextMeshPro。

## 如何運行此專案
1. 將此儲存庫Clone到您的本地端電腦：
   ```bash
   git clone [https://github.com/huang101007/CubeSurvivors-3D-Game.git](https://github.com/huang101007/CubeSurvivors-3D-Game.git)
   ```

2. 打開Unity Hub。

3. 點擊Add並選擇剛剛Clone下來的資料夾。

4. 打開位於Assets/Scenes/底下的MainMenuScene場景。

5. 在Unity編輯器中按下Play即可開始遊戲！

## 專案結構
* Assets/Scripts/：核心C#邏輯(包含GameManager、玩家與敵人控制器、升級系統)。

* Assets/Prefabs/：預先設定好的GameObjects(包含Player、Enemies、UI面板與Bullets)。

* Assets/Scenes/：存放MainMenuScene與GameScene_Level1。
