# CubeSurvivors-3D-Game
使用Unity開發的3D俯視角Roguelite生存遊戲。

## 遊戲簡介
在Cube Survivors中，玩家必須在封閉的競技場中無盡的敵人攻勢下生存。透過擊殺敵人，玩家可以收集EXP寶石來升級並獲取隨機的能力強化。撐到倒數計時結束即可獲得勝利！

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
