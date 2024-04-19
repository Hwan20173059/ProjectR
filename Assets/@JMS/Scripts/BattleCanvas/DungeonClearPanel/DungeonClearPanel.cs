using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonClearPanel : MonoBehaviour
{
    BattleCanvas battleCanvas;
    DungeonClearButton dungeonClearButton;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        dungeonClearButton = GetComponentInChildren<DungeonClearButton>();

        dungeonClearButton.button.onClick.AddListener(TownSceneLoad);

        gameObject.SetActive(false);
    }

    void TownSceneLoad()
    {
        gameObject.SetActive(false);
        if (PlayerManager.Instance.isField == true)
        {
            AudioManager.Instance.SetState();
            battleCanvas.CloseScreen("FieldScene");
        }
        else if (PlayerManager.Instance.isDungeon == true)
        {
            AudioManager.Instance.SetState();
            battleCanvas.CloseScreen("DungeonScene");
        }
        
    }
}
