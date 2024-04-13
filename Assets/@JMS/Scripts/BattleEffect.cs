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

    public void SetMoveEffect(int id)
    {
        StartCoroutine(MoveEffect(id));
    }

    IEnumerator DurationEffect(int id)
    {
        ChangeAnimSet(id);

        transform.localScale = new Vector3(5, 5) * DataManager.Instance.effectDatabase.GetDataByKey(id).effectScale;
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

    IEnumerator MoveEffect(int id)
    {
        ChangeAnimSet(id);

        float speed = DataManager.Instance.effectDatabase.GetDataByKey(id).moveSpeed;
        Vector3 movePos = new Vector3(DataManager.Instance.effectDatabase.GetDataByKey(id).movePosX, DataManager.Instance.effectDatabase.GetDataByKey(id).movePosY);

        while (MoveTowardsEffect(movePos, speed)) { yield return null; }

        gameObject.SetActive(false) ;
    }

    private bool MoveTowardsEffect(Vector3 target, float speed)
    {
        return target != (transform.position =
            Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime));
    }

    void ChangeAnimSet(int id)
    {
        animator.runtimeAnimatorController = DataManager.Instance.effectDatabase.GetAnimDataByKey(id);
    }
}
