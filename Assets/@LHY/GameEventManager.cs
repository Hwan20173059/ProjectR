using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //юс╫ц
    public static GameEventManager instance { get; private set; }
    
    public QuestEvent questEvent;

    private void Awake()
    {
        instance = this;

        questEvent = new QuestEvent();
    }
}
