using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
    public void SetRepeatEffect(int id, float effectScale)
    {
        StartCoroutine(RepeatEffect(id, effectScale));
    }
    public void SetMoveEffect(int id)
    {
        StartCoroutine(MoveEffect(id));
    }
    public void SetMoveEffect(int id, Vector3 targetPos)
    {
        StartCoroutine(MoveEffect(id, targetPos));
    }
    public void SetEffect(int id)
    {
        Effect(id);
    }
    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
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
    IEnumerator RepeatEffect(int id, float effectScale)
    {
        ChangeAnimSet(id, effectScale);

        float repeatingCount = DataManager.Instance.effectDatabase.GetDataByKey(id).repeatingCount;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < repeatingCount)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }

    IEnumerator MoveEffect(int id)
    {
        ChangeAnimSet(id);

        Vector3 startPos = transform.position;
        Vector3 movePos = new Vector3(DataManager.Instance.effectDatabase.GetDataByKey(id).movePosX, DataManager.Instance.effectDatabase.GetDataByKey(id).movePosY);
        Vector3 targetPos = startPos + movePos;
        float moveTIme = 0;
        float moveSeconds = DataManager.Instance.effectDatabase.GetDataByKey(id).moveSeconds;

        while (LerpEffect(startPos, targetPos, moveTIme))
        {
            moveTIme += Time.deltaTime / moveSeconds;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    IEnumerator MoveEffect(int id, Vector3 targetPos)
    {
        ChangeAnimSet(id);

        Vector3 startPos = transform.position;
        float moveTIme = 0;
        float moveSeconds = DataManager.Instance.effectDatabase.GetDataByKey(id).moveSeconds;

        while (LerpEffect(startPos, targetPos, moveTIme))
        {
            moveTIme += Time.deltaTime / moveSeconds;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private bool LerpEffect(Vector3 startPos, Vector3 targetPos, float moveTime)
    {
        return targetPos != (transform.position =
            Vector3.Lerp(startPos, targetPos, moveTime));
    }

    void Effect(int id)
    {
        ChangeAnimSet(id);
    }

    void ChangeAnimSet(int id)
    {
        animator.runtimeAnimatorController = DataManager.Instance.effectDatabase.GetAnimDataByKey(id);
        transform.localScale = new Vector3(5, 5) * DataManager.Instance.effectDatabase.GetDataByKey(id).effectScale;
    }
    void ChangeAnimSet(int id, float effectScale)
    {
        animator.runtimeAnimatorController = DataManager.Instance.effectDatabase.GetAnimDataByKey(id);
        transform.localScale = new Vector3(5, 5) * effectScale;
    }
}
