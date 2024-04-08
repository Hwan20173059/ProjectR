using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllQuestSaveData
{
    public List<SaveQuestData> questSaveData;
}

[System.Serializable]
public class SaveQuestData
{
    public int questID;
    public QuestState questState;
    public int questCurrentValue;
}
