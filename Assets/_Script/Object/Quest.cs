using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Object/quest Data")]
public class Quest : ScriptableObject
{
    [Header("info")]
    public string id;
    public string questName;
    public string questExplain;
    public string[] scripts;

    [Header("require")]
    public int currentQuestValue;
    public int requireQuestValue;
    public bool isClear;

    [Header("reward")]
    public Item itemReward;
    public int goldReward;
    public int expReward;
}
