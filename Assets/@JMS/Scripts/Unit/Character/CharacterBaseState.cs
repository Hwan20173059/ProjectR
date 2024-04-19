using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseState : IState
{
    protected CharacterStateMachine stateMachine;
    protected Character character { get { return stateMachine.character; } }
    protected BattleManager battleManager { get { return character.battleManager; } }
    protected Monster selectMonster { get { return battleManager.selectMonster; } set { battleManager.selectMonster = value; } }
    protected List<EquipItem> rouletteResult { get{ return battleManager.rouletteEquip; } }
    public CharacterBaseState(CharacterStateMachine characterStateMachine)
    {
        stateMachine = characterStateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Debug.DrawRay(character.transform.position, Vector3.right * 4f, new Color(0, 100, 0));
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool MoveTowardsCharacter(Vector3 target)
    {
        return target != (character.transform.position =
            Vector3.MoveTowards(character.transform.position, target, character.moveAnimSpeed * Time.deltaTime));
    }

    protected bool LerpCharacter(Vector3 startPos, Vector3 target, float curMoveTime)
    {
        return target != (character.transform.position =
            Vector3.MoveTowards(startPos, target, curMoveTime));
    }

    protected void StateUpdate(string state)
    {
        character.currentStateText = state;
        if (battleManager != null)
            battleManager.battleCanvas.UpdateCharacterState();
    }
    protected void AtkUpdate(string state)
    {
        character.currentStateText = state;
        if(battleManager != null)
            battleManager.battleCanvas.UpdateCharacterAtk();
    }
}
