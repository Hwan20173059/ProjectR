using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;

    public ItemManager itemManager;
    public GameObject RewardUI;
    public GameObject RewardSlotPrefeb;
    public GameObject RewardTrans;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Rewading(List<ItemReward> reward, int gold, int exp)
    {
        goldReward(gold);
        expReward(exp);

        for (int i = 0; i < reward.Count; i++)
        {
            if (reward[i].itemProbability > Random.Range(0, 100))
            {
                itemManager.AddEquipItem(reward[i].itemID);
                itemManager.AddConsumeItem(reward[i].itemID);
                if (reward[i].ItemType.Equals(ItemType.Consume))
                {
                    itemManager.AddConsumeItem(reward[i].itemID);
                }
                GameObject RewardSlot = Instantiate(RewardSlotPrefeb);
                RewardSlot.transform.parent = RewardTrans.transform;

                //id 하나로 다 가져와야함.
                RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = DataManager.Instance.itemDatabase.GetItemByKey(i).equipName;
                RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(i).spritePath, typeof(Sprite)) as Sprite;
            }
        }
        PopupReward();
    }

    private void goldReward(int gold)
    {
        GameObject RewardSlot = Instantiate(RewardSlotPrefeb);
        RewardSlot.transform.parent = RewardTrans.transform;
        RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = gold.ToString();

        //todo : GOLD Sprite 적용
        //RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(i).spritePath, typeof(Sprite)) as Sprite;

    }

    private void expReward(int exp)
    {
        GameObject RewardSlot = Instantiate(RewardSlotPrefeb);
        RewardSlot.transform.parent = RewardTrans.transform;
        RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = exp.ToString();

        //todo : EXP Sprite 적용(소스가 있음?)
        //RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(i).spritePath, typeof(Sprite)) as Sprite;
    }
    private void PopupReward()
    {
        RewardUI.SetActive(true);
    }
}
