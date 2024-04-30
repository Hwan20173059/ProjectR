using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleVictoryButton : MonoBehaviour
{
    BattleCanvas battleCanvas;

    Button button;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        button = GetComponent<Button>();
        button.onClick.AddListener(SceneLoad);
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
