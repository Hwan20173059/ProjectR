using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnTownButton : MonoBehaviour
{
    BattleCanvas battleCanvas;

    Button button;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        button = GetComponent<Button>();
        button.onClick.AddListener(TownSceneLoad);
    }

    void TownSceneLoad()
    {
        gameObject.SetActive(false);
        battleCanvas.CloseScreen("TownScene");
        AudioManager.Instance.SetState();
    }
}
