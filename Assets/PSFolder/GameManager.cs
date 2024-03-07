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

public class GameManager : MonoBehaviour
{
    public PlayerState playerState;
    public List<EquipItem> eInventory;
    public static GameManager instance;

    private void Awake() 
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }
}
