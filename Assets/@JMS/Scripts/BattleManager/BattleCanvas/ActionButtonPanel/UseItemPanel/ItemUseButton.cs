using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseButton : MonoBehaviour
{
    BattleCanvas battleCanvas;

    Button button;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickItemUseButton);
    }

    private void OnClickItemUseButton()
    {
        battleCanvas.OnClickItemUseButton();
    }
}
