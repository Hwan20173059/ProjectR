using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    //RewardUI�� ����� ������ Display�� �Ҳ���.
    //SO������ ����Ŭ��� ���� ���� ����س���
    //������(�̰� �Һ������ ����?)�̶� ����� �Ҳ���
    //����Ʈ�� �޾ƿͼ� ���⼭ �����ؼ� ������UI�� �Ѹ�����.


    public GameObject endPanel;
    DungeonManager dungeonManager;
    public GameObject rewardSlotPrefeb;
    Sprite rewardSprite;
    string rewardName;
        
   //EXP�� ����..?

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
