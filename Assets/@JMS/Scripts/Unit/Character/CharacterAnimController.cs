using System;
using UnityEngine;

public enum CharacterAnim
{
    Idle,
    Attack,
    Push,
    Jab,
    Slash,
    Shot,
    Fire1H,
    Fire2H,
    Jump,
    Hit,
    Heal
}

public enum CharacterAnimState
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

public class CharacterAnimController : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnim(CharacterAnim anim)
    {
        switch (anim)
        {
            case CharacterAnim.Idle: animator.SetTrigger("Idle"); break;
            case CharacterAnim.Attack: animator.SetTrigger("Attack"); break;
            case CharacterAnim.Push: animator.SetTrigger("Push"); break;
            case CharacterAnim.Jab: animator.SetTrigger("Jab"); break;
            case CharacterAnim.Slash: animator.SetTrigger("Slash"); break;
            case CharacterAnim.Shot: animator.SetTrigger("Shot"); break;
            case CharacterAnim.Fire1H: animator.SetTrigger("Fire1H"); break;
            case CharacterAnim.Fire2H: animator.SetTrigger("Fire2H"); break;
            case CharacterAnim.Jump: animator.SetTrigger("Jump"); break;
            case CharacterAnim.Hit: animator.SetTrigger("Hit"); break;
            case CharacterAnim.Heal: animator.SetTrigger("Heal"); break;
        }
    }

    public void ChangeAnimState(CharacterAnimState animState)
    {
        foreach (var variable in new[] { "Ready", "Walking", "Running", "Resting", "Crawling", "Climbing", "Blocking", "GetDown", "Dead" })
        {
           animator.SetBool(variable, false);
        }

        switch (animState)
        {
            case CharacterAnimState.Idle: animator.SetTrigger("Idle"); break;
            case CharacterAnimState.Ready: animator.SetBool("Ready", true); break;
            case CharacterAnimState.Walking: animator.SetBool("Walking", true); break;
            case CharacterAnimState.Running: animator.SetBool("Running", true); break;
            case CharacterAnimState.Resting: animator.SetBool("Resting", true); break;
            case CharacterAnimState.Crawling: animator.SetBool("Crawling", true); break;
            case CharacterAnimState.Climbing: animator.SetBool("Climbing", true); break;
            case CharacterAnimState.Blocking: animator.SetBool("Blocking", true); break;
            case CharacterAnimState.GetDown: animator.SetBool("GetDown", true); break;
            case CharacterAnimState.Dead: animator.SetBool("Dead", true); break;
        }
    }
}