using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject equipInventory;
    public NowEquippedItemSlot nEquipItemSlot;
    public DetailArea detailArea;
    public EquipItem nowSelectedEquip;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void OpenInventory()
    {
        equipInventory.SetActive(true);
    }
    public void CloseInventory()
    {
        equipInventory.SetActive(false);
    }
}
