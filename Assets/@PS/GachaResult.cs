using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResult : MonoBehaviour
{
    public GameObject closeButton;

    public GameObject gacha1UI;
    public GameObject gacha10UI;
    public RectTransform scrollInitialize;
    public GachaSlot[] gachaSlots10 = new GachaSlot[10];

    private bool[] isHaving = new bool[10];
    private bool isChar;

    public void Equip1UI(EquipItem e, bool isHaving)
    {
        if (isHaving)
        {
            gacha1UI.GetComponentInChildren<GachaSlot>().PlayAnim("CoinAnim");
        }
        else
        {
            gacha1UI.GetComponentInChildren<GachaSlot>().SetImage(e.equipSprite);
        }
        closeButton.SetActive(true);
        gameObject.SetActive(true);
        gacha1UI.SetActive(true);
    }

    public void Equip10UI(EquipItem[] e, bool[] isHaving)
    {
        this.isHaving = isHaving;
        isChar = false;
        for (int i = 0; i < e.Length; i++)
        {
            gacha10UI.GetComponentsInChildren<GachaSlot>()[i].SetImage(e[i].equipSprite);
        }
        for (int i = 0; i < 10; i++)
        {
            gachaSlots10[i].gameObject.SetActive(false);
        }
        scrollInitialize.anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
        gacha10UI.SetActive(true);
        StartCoroutine(ActiveSlots());
    }

    public void Character1UI(ConsumeItem c, bool isHaving)
    {
        if (isHaving)
        {
            gacha1UI.GetComponentInChildren<GachaSlot>().PlayAnim("ScrollAnim");
        }
        else
        {
            gacha1UI.GetComponentInChildren<GachaSlot>().SetImage(c.consumeSprite);
        }
        closeButton.SetActive(true);
        gameObject.SetActive(true);
        gacha1UI.SetActive(true);
    }

    public void Character10UI(ConsumeItem[] c, bool[] isHaving)
    {
        this.isHaving = isHaving;
        isChar = true;
        for (int i = 0; i < c.Length; i++)
        {
            gacha10UI.GetComponentsInChildren<GachaSlot>()[i].SetImage(c[i].consumeSprite);
        }
        for (int i = 0; i < 10; i++)
        {
            gachaSlots10[i].gameObject.SetActive(false);
        }
        scrollInitialize.anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
        gacha10UI.SetActive(true);
        StartCoroutine(ActiveSlots());
    }

    public void ChangeSlotToCoin()
    {
        for(int i = 0; i < 10; i++)
        {
            if (isHaving[i]) gachaSlots10[i].PlayAnim("CoinAnim");
        }
    }

    public void ChangeSlotToExp()
    {
        for (int i = 0; i < 10; i++)
        {
            if (isHaving[i]) gachaSlots10[i].PlayAnim("ScrollAnim");
        }
    }

    public IEnumerator ActiveSlots()
    {
        for (int i = 0; i < 10; i++)
        {
            gachaSlots10[i].gameObject.SetActive(true);
            if (i == 9)
            {
                closeButton.SetActive(true);
                if (isChar) ChangeSlotToExp();
                else ChangeSlotToCoin();
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void CloseResultPopup()
    {
        gameObject.SetActive(false);
        gacha1UI.SetActive(false);
        gacha10UI.SetActive(false);
        closeButton.SetActive(false);
    }
}
