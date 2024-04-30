using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleDefeatPanel : MonoBehaviour
{
    ReturnTownButton returnTownButton;

    public void Init(BattleCanvas battleCanvas)
    {
        returnTownButton = GetComponentInChildren<ReturnTownButton>();
        returnTownButton.Init(battleCanvas);

        gameObject.SetActive(false);
    }
}
