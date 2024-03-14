using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectQuestUI : MonoBehaviour
{
    
    public string selectQuestId;

    private void Awake()
    {

    }
    public void setQuestID(string questId)
    {
        selectQuestId = questId;
    }

    public void SubmitOnClick()
    {
        Debug.Log(selectQuestId + "");
        GameEventManager.instance.questEvent.SubmitPressed(selectQuestId);
    }
}
