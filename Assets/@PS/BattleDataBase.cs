using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleDataBase
{
    public List<CharacterData> characterDatas;
    public Dictionary<int, CharacterData> characterDic = new();

    public List<MonsterData> monsterDatas;
    public Dictionary<int, MonsterData> monsterDic = new();

    public List<StageData> stageDatas;
    public Dictionary<int, StageData> stageDic = new();

    public List<DungeonData> dungeonDatas;
    public Dictionary<int, DungeonData> dungeonDic = new();


    public void Initialize()
    {
        foreach (CharacterData characterBase in characterDatas)
        {
            characterDic.Add(characterBase.id, characterBase);
        }

        foreach (MonsterData monsterBase in monsterDatas)
        {
            monsterDic.Add(monsterBase.id, monsterBase);
        }

        foreach (StageData stageBase in stageDatas)
        {
            stageDic.Add(stageBase.id, stageBase);
        }

        foreach (DungeonData dungeonBase in dungeonDatas)
        {
            dungeonDic.Add(dungeonBase.id, dungeonBase);
        }
    }

    public CharacterData GetCharacterByKey(int id)
    {
        if (characterDic.ContainsKey(id))
            return characterDic[id];

        return null;
    }

    public MonsterData GetMonsterByKey(int id)
    {
        if (monsterDic.ContainsKey(id))
            return monsterDic[id];

        return null;
    }

    public StageData GetStageByKey(int id)
    {
        if (stageDic.ContainsKey(id))
            return stageDic[id];

        return null;
    }

    public DungeonData GetDungeonByKey(int id)
    {
        if (dungeonDic.ContainsKey(id))
            return dungeonDic[id];

        return null;
    }
}
