using System;
using UnityEngine;

public enum TestCharacterAnims
{
    Attack,
    Push,
    Jab,
    Slash,
    Shot,
    Fire1H,
    Fire2H,
    Hit,
    Heal
}

public enum TestCharacterAnimState
{
    Idle,
    Ready,
    Walking,
    Running,
    Resting,
    Crawling,
    Climbing,
    Blocking,
    GetDown,
    Dead
}

public class TestCharacterAnimController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim(TestCharacterAnims anim)
    {
        switch (anim)
        {
            case TestCharacterAnims.Attack: animator.SetTrigger("Attack"); break;
            case TestCharacterAnims.Push: animator.SetTrigger("Push"); break;
            case TestCharacterAnims.Jab: animator.SetTrigger("Jab"); break;
            case TestCharacterAnims.Slash: animator.SetTrigger("Slash"); break;
            case TestCharacterAnims.Shot: animator.SetTrigger("Shot"); break;
            case TestCharacterAnims.Fire1H: animator.SetTrigger("Fire1H"); break;
            case TestCharacterAnims.Fire2H: animator.SetTrigger("Fire2H"); break;
            case TestCharacterAnims.Hit: animator.SetTrigger("Hit"); break;
            case TestCharacterAnims.Heal: animator.SetTrigger("Heal"); break;
        }
    }

    public void ChangeAnimState(TestCharacterAnimState animState)
    {
        foreach (var variable in new[] { "Ready", "Walking", "Running", "Resting", "Crawling", "Climbing", "Blocking", "GetDown", "Dead" })
        {
           animator.SetBool(variable, false);
        }

        switch (animState)
        {
            case TestCharacterAnimState.Idle: break;
            case TestCharacterAnimState.Ready: animator.SetBool("Ready", true); break;
            case TestCharacterAnimState.Walking: animator.SetBool("Walking", true); break;
            case TestCharacterAnimState.Running: animator.SetBool("Running", true); break;
            case TestCharacterAnimState.Resting: animator.SetBool("Resting", true); break;
            case TestCharacterAnimState.Crawling: animator.SetBool("Crawling", true); break;
            case TestCharacterAnimState.Climbing: animator.SetBool("Climbing", true); break;
            case TestCharacterAnimState.Blocking: animator.SetBool("Blocking", true); break;
            case TestCharacterAnimState.GetDown: animator.SetBool("GetDown", true); break;
            case TestCharacterAnimState.Dead: animator.SetBool("Dead", true); break;
        }
    }
}