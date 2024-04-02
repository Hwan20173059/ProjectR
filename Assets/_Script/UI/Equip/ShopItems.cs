using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public List<ConsumeItem> shopItemList;

    [SerializeField] protected Transform shopParent;
    [SerializeField] protected List<ShopSlot> shopSlots;

    private void OnValidate()
    {
        shopParent.GetComponentsInChildren<ShopSlot>(includeInactive: true, result: shopSlots);
    }

    private void Start()
    {
        DataManager.Instance.Init();
        for(int i = 23;  i <= 31; i++)
        {
            shopItemList.Add(new ConsumeItem(DataManager.Instance.itemDatabase.GetCItemByKey(i)));
        }
        FreshShop();
    }

    public void FreshShop() // reload slots & show items
    {
        int i = 0;
        for (; i < shopItemList.Count && i < shopSlots.Count; i++)
        {
            shopSlots[i].item = shopItemList[i];
        }
        for (; i < shopSlots.Count; i++)
        {
            shopSlots[i].item = null;
        }
    }
}
