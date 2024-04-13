using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffect : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDurationEffect(int id)
    {
        StartCoroutine(DurationEffect(id));
    }
    public void SetRepeatEffect(int id)
    {
        StartCoroutine(RepeatEffect(id));
    }

    IEnumerator DurationEffect(int id)
    {
        ChangeAnimSet(id);

        float effectDuration = DataManager.Instance.effectDatabase.GetDataByKey(id).duration;
        float pastTime = 0;

        while (pastTime < effectDuration)
        {
            pastTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    IEnumerator RepeatEffect(int id)
    {
        ChangeAnimSet(id);

        float repeatingCount = DataManager.Instance.effectDatabase.GetDataByKey(id).repeatingCount;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < repeatingCount)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }

    void ChangeAnimSet(int id)
    {
        animator.runtimeAnimatorController = DataManager.Instance.effectDatabase.GetAnimDataByKey(id);
    }
}