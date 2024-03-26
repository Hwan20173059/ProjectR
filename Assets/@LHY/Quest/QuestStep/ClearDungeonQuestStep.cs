using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//todo : 구독
public class ClearDungeonQuestStep : QuestStep
{
    public int dungeonCleared = 0;
    private int dungeonComplete = 1;

    public QuestState currentState = QuestState.In_Progress;


    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventManager.instance.battleEvent.onDungeonClear += DungeonClear;
        GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventManager.instance.battleEvent.onDungeonClear -= DungeonClear;
        GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;
    }
    //todo : 던전 클리어 체크
    public void DungeonClear()//todo : private화
    {
        dungeonCleared++;
        if (dungeonCleared < dungeonComplete)
        {
            UpdateState();
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest("ClearDungeonQuest");
        }
    }


    public void FinishQuest(string id)
    {
        if (id != gameObject.name)
        {
            //
        }
        FinishQuestStep();
    }

    //사용하지 않음.
    private void UpdateState()
    {
        print("state업데이트");
        string state = dungeonCleared.ToString();
        string status = dungeonCleared + " / " + dungeonComplete;
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.dungeonCleared = int.Parse(state);
        UpdateState();
    }
}
