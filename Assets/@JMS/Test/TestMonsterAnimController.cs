using System;
using UnityEngine;

public enum TestMonsterAnims
{
    Attack,
    Hit,
    Heal
}

public enum TestMonsterAnimState
{
    Idle,
    Ready,
    Walking,
    Running,
    Dead
}

public class TestMonsterAnimController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim(TestMonsterAnims anim)
    {
        switch (anim)
        {
            case TestMonsterAnims.Attack: animator.SetTrigger("Attack"); break;
            case TestMonsterAnims.Hit: animator.SetTrigger("Hit"); break;
            case TestMonsterAnims.Heal: animator.SetTrigger("Heal"); break;
        }
    }

    public void ChangeAnimState(TestMonsterAnimState animState)
    {
        foreach (var variable in new[] { "Ready", "Walking", "Running", "Dead" })
        {
           animator.SetBool(variable, false);
        }

        switch (animState)
        {
            case TestMonsterAnimState.Idle: break;
            case TestMonsterAnimState.Ready: animator.SetBool("Ready", true); break;
            case TestMonsterAnimState.Walking: animator.SetBool("Walking", true); break;
            case TestMonsterAnimState.Running: animator.SetBool("Running", true); break;
            case TestMonsterAnimState.Dead: animator.SetBool("Dead", true); break;
        }
    }
}