using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject equipInventory;
    public NowEquippedItemSlot nEquipItemSlot;
    public DetailArea detailArea;
    public EquipItem nowSelectedEquip;

    public void OpenInventory()
    {
        equipInventory.SetActive(true);
    }
    public void CloseInventory()
    {
        equipInventory.SetActive(false);
    }
}
