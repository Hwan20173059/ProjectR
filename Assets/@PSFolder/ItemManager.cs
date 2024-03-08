using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class EquipItem
{
    public bool isEquipped;
    public Equip data;
}

public class ItemManager : MonoBehaviour
{
    public EquipItem baseItem;
    [SerializeField] private Equip baseEquip;
    public PlayerState playerState;
    public List<EquipItem> eInventory;
    public static ItemManager instance;

    private void Awake() 
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        baseItem.data = baseEquip;
    }
}
