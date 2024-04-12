using System;
using UnityEngine;

public enum MonsterAnim
{
    Attack,
    Hit,
    Heal
}

public enum MonsterAnimState
{
    Idle,
    Ready,
    Walking,
    Running,
    Dead
}

public class MonsterAnimController : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim(MonsterAnim anim)
    {
        switch (anim)
        {
            case MonsterAnim.Attack: animator.SetTrigger("Attack"); break;
            case MonsterAnim.Hit: animator.SetTrigger("Hit"); break;
            case MonsterAnim.Heal: animator.SetTrigger("Heal"); break;
        }
    }

    public void ChangeAnimState(MonsterAnimState animState)
    {
        foreach (var variable in new[] { "Ready", "Walking", "Running", "Dead" })
        {
           animator.SetBool(variable, false);
        }

        switch (animState)
        {
            case MonsterAnimState.Idle: break;
            case MonsterAnimState.Ready: animator.SetBool("Ready", true); break;
            case MonsterAnimState.Walking: animator.SetBool("Walking", true); break;
            case MonsterAnimState.Running: animator.SetBool("Running", true); break;
            case MonsterAnimState.Dead: animator.SetBool("Dead", true); break;
        }
    }
}