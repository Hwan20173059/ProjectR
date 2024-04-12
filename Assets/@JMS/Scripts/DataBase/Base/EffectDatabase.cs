using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDatabase : IDatabase
{
    public List<EffectData> effectDatas;
    Dictionary<int, EffectData> effectDic = new();
    Dictionary<int, RuntimeAnimatorController> animDic = new();

    public void Initialize()
    {
        foreach (EffectData data in effectDatas)
        {
            effectDic.Add(data.id, data);
        }

        for (int i = 0; i < effectDic.Count; i++)
        {
            animDic.Add(effectDic[i].id, Resources.Load<RuntimeAnimatorController>(effectDic[i].animatorPath));
        }
    }

    public EffectData GetDataByKey(int id)
    {
        if (effectDic.ContainsKey(id))
            return effectDic[id];

        return null;
    }

    public RuntimeAnimatorController GetAnimDataByKey(int id)
    {
        if (animDic.ContainsKey(id))
            return animDic[id];

        return null;
    }
}
