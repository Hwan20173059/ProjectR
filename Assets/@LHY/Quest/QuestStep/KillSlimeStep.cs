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
        UpdateState();//Json에 데이터 저장 구현중..
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

    //todo : 던전 클리어 체크
    public void KillSlime()//todo : private화
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

    //Json 데이터 저장
    private void UpdateState()
    {
        print("state업데이트");
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
