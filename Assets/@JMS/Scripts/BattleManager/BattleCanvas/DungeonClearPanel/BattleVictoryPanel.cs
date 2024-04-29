using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleVictoryPanel : MonoBehaviour
{
    BattleCanvas battleCanvas;
    BattleVictoryButton battleVictoryButton;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        battleVictoryButton = GetComponentInChildren<BattleVictoryButton>();

        battleVictoryButton.button.onClick.AddListener(SceneLoad);

        gameObject.SetActive(false);
    }

    void SceneLoad()
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
