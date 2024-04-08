using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//todo : ����
public class EnterDungeonQuestStep : QuestStep
{
    public int dungeonCleared = 0;
    private int dungeonComplete = 5;



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
    //todo : ���� Ŭ���� üũ
    public void DungeonClear()//todo : privateȭ
    {
        if (dungeonCleared < dungeonComplete)
        {
            print("���� Ŭ����");
            dungeonCleared++;
            UpdateState();
        }
        else
        {
            print("finish");
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        print("state������Ʈ");
        string state = dungeonCleared.ToString();
        string status = "Collected " + dungeonCleared + " / " + dungeonComplete + " coins.";
        ChangeState(state, status);
    }


}
