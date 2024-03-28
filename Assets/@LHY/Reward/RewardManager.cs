using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;

    public ItemManager itemManager;
    int i;
    public Reward reward;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        itemManager = GetComponent<ItemManager>();
    }

    public void Rewading()
    {
        itemManager.AddEquipItem(0);
        itemManager.AddEquipItem(0);

    }
}
