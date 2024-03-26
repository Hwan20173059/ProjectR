using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//todo : ����
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
    //todo : ���� Ŭ���� üũ
    public void DungeonClear()//todo : privateȭ
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

    //������� ����.
    private void UpdateState()
    {
        print("state������Ʈ");
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
