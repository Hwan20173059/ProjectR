using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp : MonoBehaviour
{
    public void submitclick()
    {
        GameEventManager.instance.questEvent.SubmitPressed();
    }

    public void dungeonclick()
    {
        GameEventManager.instance.questEvent.DungeonClear();
        //GameEventManager.instance.questEvent.QuestStateChange();
    }
}
