using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tmp : MonoBehaviour
{
    

    public void dungeonclick()
    {
        GameEventManager.instance.battleEvent.DungeonClear();
        //GameEventManager.instance.questEvent.QuestStateChange();
    }

    public void dungeonenter(string str)
    {
        SceneManager.LoadScene(str);
    }


}
