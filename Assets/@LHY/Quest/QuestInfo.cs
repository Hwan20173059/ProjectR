using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestInfo
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;
    public string questDescription;

    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefebs;
    //todo : 변경된 계획) 연결되는 퀘스트는 General in NextID를 통해 요구사항 체크 퀘스트로 반영
    // 그래서 현재 배열일 이유 없긴함.


    [Header("Rewards")]
    public int goldReward;
    public int expReward;
    public ConsumeReward[] consumeRewards;
    public EquipReward[] equipRewards;
    //public Item itemID;??????????????
    //todo : 보상에 아이템 추가

    private void OnValidate()
    {
        //일단 보류 ID 교체 예정


        //Unity상에서 파일명이랑 id를 고정시켜버리자.
        //id는 퀘스트의 key로써 너무 중요하고 다를 필요도 없음.
        //#if UNITY_EDITOR
        //id = this.name;
        //UnityEditor.EditorUtility.SetDirty(this);
        //#endif
    }
    private void Awake()
    {

    }
}
