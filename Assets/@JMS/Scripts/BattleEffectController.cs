using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BattleEffectController
{
    ObjectPool objectPool;

    public BattleEffectController(ObjectPool objectPool)
    {
        this.objectPool = objectPool;
    }

    public void SetDurationEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + (Vector3.up / 2);
        go.GetComponent<BattleEffect>().SetDurationEffect(id);
    }

    public void SetRepeatEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + (Vector3.up / 2);
        go.GetComponent<BattleEffect>().SetRepeatEffect(id);
    }
    public void SetRepeatEffect(int id, float effectScale, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + (Vector3.up / 2);
        go.GetComponent<BattleEffect>().SetRepeatEffect(id, effectScale);
    }

    public void SetMoveEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + (Vector3.up / 2);
        go.GetComponent<BattleEffect>().SetMoveEffect(id);
    }
    public void SetMoveEffect(int id, Vector3 startPos, Vector3 targetPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + (Vector3.up / 2);
        go.GetComponent<BattleEffect>().SetMoveEffect(id, targetPos);
    }
    public void SetMoveEffect(int id, Vector3 startPos, Vector3 targetPos, float angle)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + (Vector3.up / 2);
        go.GetComponent<BattleEffect>().SetMoveEffect(id, targetPos, angle);
    }

    public BattleEffect SetEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos + Vector3.up;
        BattleEffect effect = go.GetComponent<BattleEffect>();
        effect.SetEffect(id);
        return effect;
    }

    public void BattleEffectOff()
    {
        objectPool.SetActiveFalseAll("BattleEffect");
    }
}
