using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResult : MonoBehaviour
{
    public ItemManager itemManager;
    public Sprite coin;
    public GameObject gacha1UI;
    public GameObject gacha10UI;
    public RectTransform scrollInitialize;

    public void Equip1UI(EquipItem e, bool isHaving)
    {
        if (isHaving)
        {
            gacha1UI.GetComponentInChildren<GachaSlot>().SetImage(coin);
        }
        else
        {
            gacha1UI.GetComponentInChildren<GachaSlot>().SetImage(e.equipSprite);
        }
        gameObject.SetActive(true);
        gacha1UI.SetActive(true);
    }

    public void Equip10UI(EquipItem[] e, bool[] isHaving)
    {
        for (int i = 0; i < e.Length; i++)
        {
            if (isHaving[i])
            {
                gacha10UI.GetComponentsInChildren<GachaSlot>()[i].SetImage(coin);
            }
            else
            {
                gacha10UI.GetComponentsInChildren<GachaSlot>()[i].SetImage(e[i].equipSprite);
            }
        }
        scrollInitialize.anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
        gacha10UI.SetActive(true);
    }

    public void Character1UI(ConsumeItem c)
    {
        gacha1UI.GetComponentInChildren<GachaSlot>().SetImage(c.consumeSprite);
        gameObject.SetActive(true);
        gacha1UI.SetActive(true);
    }

    public void Character10UI(ConsumeItem[] c)
    {
        for (int i = 0; i < c.Length; i++)
        {
            gacha10UI.GetComponentsInChildren<GachaSlot>()[i].SetImage(c[i].consumeSprite);
        }
        scrollInitialize.anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
        gacha10UI.SetActive(true);
    }

    public void CloseResultPopup()
    {
        gameObject.SetActive(false);
        gacha1UI.SetActive(false);
        gacha10UI.SetActive(false);
    }
}
