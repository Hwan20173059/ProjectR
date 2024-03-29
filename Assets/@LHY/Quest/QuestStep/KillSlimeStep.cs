using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSlimeStep : QuestStep
{
    public int killSlimeCount = 0;
    private int killSlimeComplete = 5;

    public QuestState currentState = QuestState.In_Progress;


    private void Start()
    {
        UpdateState();//Json�� ������ ���� ������..
    }

    private void OnEnable()
    {
        //GameEventManager.instance.battleEvent.onKillSlime += KillSlime;
        GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        //GameEventManager.instance.battleEvent.onKillSlime -= KillSlime;
        GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;
    }

    //todo : ���� Ŭ���� üũ
    public void KillSlime()//todo : privateȭ
    {
        killSlimeCount++;
        if (killSlimeCount < killSlimeComplete)
        {
            UpdateState();
        }
        else
        {
            GameEventManager.instance.questEvent.AdvanceQuest("KillSlime");
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

    //Json ������ ����
    private void UpdateState()
    {
        print("state������Ʈ");
        string state = killSlimeCount.ToString();
        string status = killSlimeCount + " / " + killSlimeComplete;
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.killSlimeCount = int.Parse(state);
        UpdateState();
    }
}
