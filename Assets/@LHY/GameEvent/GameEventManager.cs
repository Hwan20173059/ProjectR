using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance { get; private set; }
    
    public QuestEvent questEvent;
    public BattleEvent battleEvent;
    public UIEvent uiEvent;

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
        uiEvent = new UIEvent();
    }
}
