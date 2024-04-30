using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteButton : MonoBehaviour
{
    BattleManager battleManager;

    Button button;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickRouletteButton);

        button.gameObject.SetActive(false);
    }

    void OnClickRouletteButton()
    {
        battleManager.OnClickRouletteButton();
    }

    public void RouletteButtonOn()
    {
        gameObject.SetActive(true);
    }
    public void RouletteButtonOff()
    {
        gameObject.SetActive(false);
    }
}
