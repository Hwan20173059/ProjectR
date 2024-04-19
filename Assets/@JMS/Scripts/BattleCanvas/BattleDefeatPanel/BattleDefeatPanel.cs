using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleDefeatPanel : MonoBehaviour
{
    BattleCanvas battleCanvas;

    ReturnTownButton returnTownButton;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        returnTownButton = GetComponentInChildren<ReturnTownButton>();
        returnTownButton.button.onClick.AddListener(TownSceneLoad);

        gameObject.SetActive(false);
    }

    void TownSceneLoad()
    {
        gameObject.SetActive(false);
        battleCanvas.CloseScreen("TownScene");
        AudioManager.Instance.SetState();
    }
}
