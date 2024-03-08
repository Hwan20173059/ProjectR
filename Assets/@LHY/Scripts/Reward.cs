using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    //RewardUI에 등록할 정보들 Display만 할꺼임.
    //SO던전에 던전클리어에 관한 보상 등록해놓고
    //아이템(이게 소비아이템 맞음?)이랑 장비템 할꺼임
    //리스트로 받아와서 여기서 정리해서 리워드UI에 뿌릴꺼임.


    public GameObject endPanel;
    DungeonManager dungeonManager;
    public GameObject rewardSlotPrefeb;
    Sprite rewardSprite;
    string rewardName;
        
   //EXP는 어케..?

    private void Awake()
    {
        dungeonManager = GetComponent<DungeonManager>();
    }

    private void Start()
    {
        Init();
    }
    void Init()
    {
        
    }
    public void AddReward(int currentDungeon)
    {
        for (int i = 0; i < dungeonManager.dungeon[currentDungeon].EquipReward.Count; i++)
        {
            rewardSprite = dungeonManager.dungeon[currentDungeon].EquipReward[i].equipSprite;
            rewardName = dungeonManager.dungeon[currentDungeon].EquipReward[i].equipName;
        }
    }
}
