using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//todo : 구독
public class ClearDungeonQuestStep : QuestStep
{
    public int dungeonCleared = 0;
    private int dungeonComplete = 0;



    private void Start()
    {
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onDungeonClear += DungeonClear;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onDungeonClear -= DungeonClear;
    }
    //todo : 던전 클리어 체크
    public void DungeonClear()//todo : private화
    {
        if (dungeonCleared < dungeonComplete)
        {
            print("던전 클리어");
            dungeonCleared++;
            UpdateState();
        }
        else
        {
            print("finish");
            FinishQuestStep();
            QuestManager.instance.currentState = QuestState.Can_Finish;
        }
    }

    private void UpdateState()
    {
        print("state업데이트");
        string state = dungeonCleared.ToString();
        string status = "Collected " + dungeonCleared + " / " + dungeonComplete + " coins.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.dungeonCleared = System.Int32.Parse(state);
        UpdateState();
    }
}
