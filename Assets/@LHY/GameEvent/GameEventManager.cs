using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //юс╫ц
    public static GameEventManager instance { get; private set; }
    
    public QuestEvent questEvent;
    public BattleEvent battleEvent;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        questEvent = new QuestEvent();
        battleEvent = new BattleEvent();
    }
}
