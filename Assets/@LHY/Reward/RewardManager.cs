using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;

    private ItemManager itemManager;
    public GameObject rewardUI;
    public GameObject rewardSlotPrefeb;
    public Transform rewardTrans;

    private Reward reward;

    private void Awake()
    {
        itemManager = ItemManager.Instance;
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // Reward가 없는 경우 -1 로 전달
    public void RewardPopup(int gold, int exp, int equipRewardID, int consumeRewardID)
    {
        //AddgoldReward(gold);
        //AddexpReward(exp);
        //AddEquipReward(equipRewardID);
        AddConsumeReward();
        PopupReward();
    }

    public void RewardPopup()
    {
        AddgoldReward();
        AddexpReward();
        AddConsumeReward();
        PopupReward();
    }

    public void AddReward(StageData stageData)
    {
        reward.gold += stageData.rewardGold;
        reward.exp += stageData.rewardExp;
        reward.consumeItemRewardID.Add(stageData.rewardItemId);
    }

    private void AddgoldReward()
    {
        GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
        RewardSlot.transform.SetParent(rewardTrans.transform);
        RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = reward.gold.ToString();
        PlayerManager.Instance.AddGold(reward.gold);


        //todo : GOLD Sprite 적용
        RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load("UiImage/Icon 5 Over", typeof(Sprite)) as Sprite;
    }

    private void AddexpReward()
    {
        GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
        RewardSlot.transform.SetParent(rewardTrans.transform);
        RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = reward.exp.ToString();
        PlayerManager.Instance.ChangeExp(reward.exp);

        //todo : EXP Sprite 적용(소스가 있음?)
        //RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(i).spritePath, typeof(Sprite)) as Sprite;
    }

    /*
    private void AddEquipReward()
    {
        //if (reward.EquipId < 0)
        //    return;
        //itemManager.AddEquipItem(reward.EquipId);
        GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
        RewardSlot.transform.SetParent(rewardTrans.transform);

        foreach (var var in reward.EquipId)
        {
            RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = DataManager.Instance.itemDatabase.GetItemByKey(var).equipName;
            RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(DataManager.Instance.itemDatabase.GetItemByKey(var).spritePath, typeof(Sprite)) as Sprite;
        
        }
    }
    */
    Consume rewardItem;
    private void AddConsumeReward()
    {
        GameObject RewardSlot = Instantiate(rewardSlotPrefeb);
        RewardSlot.transform.SetParent(rewardTrans.transform);

        foreach (int item in reward.consumeItemRewardID)
        {
            if (item < 0)
                return;
            rewardItem = DataManager.Instance.itemDatabase.GetCItemByKey(item);
            RewardSlot.GetComponentsInChildren<TextMeshProUGUI>()[0].text = rewardItem.consumeName;
            RewardSlot.GetComponentsInChildren<Image>()[1].sprite = Resources.Load(rewardItem.spritePath, typeof(Sprite)) as Sprite;
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
