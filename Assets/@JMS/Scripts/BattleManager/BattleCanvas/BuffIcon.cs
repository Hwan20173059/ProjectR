using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
    BattleCanvas battleCanvas;
    public Buff buff;

    Button button;
    Image image;
    TextMeshProUGUI typeText;

    public void Init(BattleCanvas battleCanvas, Buff buff)
    {
        this.battleCanvas = battleCanvas;
        this.buff = buff;

        button = GetComponent<Button>();
        button.onClick.AddListener(UpdateBuffText);
        image = GetComponent<Image>();
        typeText = GetComponentInChildren<TextMeshProUGUI>();

        switch (buff.type)
        {
            case BuffType.ATK:
                image.color = new Color(1, 206f / 255, 206f / 255, 1);
                typeText.text = "공격력\n버프";
                break;
            case BuffType.SPD:
                image.color = new Color(141f / 255, 193f / 255, 1, 1);
                typeText.text = "스피드\n버프";
                break;
        }
        
    }
    
    public void UpdateBuffText()
    {
        battleCanvas.UpdateBuffText(buff);
    }

}
