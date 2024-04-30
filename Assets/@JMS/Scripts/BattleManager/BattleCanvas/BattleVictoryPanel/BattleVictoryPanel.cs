using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleVictoryPanel : MonoBehaviour
{
    BattleVictoryButton battleVictoryButton;

    public void Init(BattleCanvas battleCanvas)
    {
        battleVictoryButton = GetComponentInChildren<BattleVictoryButton>();

        battleVictoryButton.Init(battleCanvas);

        gameObject.SetActive(false);
    }

}
