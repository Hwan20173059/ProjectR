using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResult : MonoBehaviour
{
    public GameObject equip1UI;
    public GameObject equip10UI;
    public RectTransform scrollInitialize;
    public GameObject character1UI;
    public GameObject character10UI;

    public void Equip1UI(EquipItem e)
    {
        equip1UI.GetComponentInChildren<GachaSlot>().SetImage(e.equipSprite);
        gameObject.SetActive(true);
        equip1UI.SetActive(true);
    }

    public void Equip10UI(EquipItem[] e)
    {
        for (int i = 0; i < e.Length; i++)
        {
            equip10UI.GetComponentsInChildren<GachaSlot>()[i].SetImage(e[i].equipSprite);
        }
        scrollInitialize.anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
        equip10UI.SetActive(true);
    }

    public void CloseResultPopup()
    {
        gameObject.SetActive(false);
        equip1UI.SetActive(false);
        equip10UI.SetActive(false);
        character1UI.SetActive(false);
        character10UI.SetActive(false);
    }
}
