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
    public GameObject rewardUI;
    public GameObject rewardSlotPrefeb;
    public Transform rewardTrans;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Rewading(EquipReward[] equipReward, ConsumeReward[] consumeReward, int gold, int exp)
    {
        AddgoldReward(gold);
        AddexpReward(exp);
        AddEquipReward(equipReward);
        AddConsumeReward(consumeReward);
        PopupReward();
    }

    private void AddgoldReward(int gold)
    {
        GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
        //RewardSlot.transform.parent = RewardTrans.transform;
        RewardSlot.transform.SetParent(rewardTrans.transform);
        RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = gold.ToString();
        PlayerManager.Instance.AddGold(gold);


        //todo : GOLD Sprite 적용
        RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load("UiImage/Icon 5 Over", typeof(Sprite)) as Sprite;

    }

    private void AddexpReward(int exp)
    {
        GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
        RewardSlot.transform.SetParent(rewardTrans.transform);
        RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = exp.ToString();
        PlayerManager.Instance.ChangeExp(exp);

        //todo : EXP Sprite 적용(소스가 있음?)
        //RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(i).spritePath, typeof(Sprite)) as Sprite;
    }

    private void AddEquipReward(EquipReward[] reward)
    {
        for (int i = 0; i < reward.Length; i++)
        {
            if (reward[i].itemProbability >= Random.Range(0, 100))
            {
                itemManager.AddEquipItem(reward[i].itemID);
                GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
                RewardSlot.transform.SetParent(rewardTrans.transform);

                //id 하나로 다 가져와야함.
                RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = DataManager.Instance.itemDatabase.GetItemByKey(i).equipName;
                RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(i).spritePath, typeof(Sprite)) as Sprite;
            }
        }

    }

    private void AddConsumeReward(ConsumeReward[] reward)
    {
        for (int i = 0; i < reward.Length; i++)
        {
            if (reward[i].itemProbability >= Random.Range(0, 100))
            {
                itemManager.AddConsumeItem(reward[i].itemID);
                GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
                RewardSlot.transform.SetParent(rewardTrans.transform);

                //id 하나로 다 가져와야함.
                RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = DataManager.Instance.itemDatabase.GetCItemByKey(i).consumeName;
                RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetCItemByKey(i).spritePath, typeof(Sprite)) as Sprite;
            }
        }

    }
    private void PopupReward()
    {
        rewardUI.SetActive(!rewardUI.activeSelf);
    }

    public void RemoveSlot()
    {
        foreach (Transform child in rewardTrans)
        {
            Destroy(child.gameObject);
        }
    }

    public void Off()
    {
        rewardUI.SetActive(!rewardUI.activeSelf);
        RemoveSlot();
        //rewardUI.SetActive(false);
    }

}
