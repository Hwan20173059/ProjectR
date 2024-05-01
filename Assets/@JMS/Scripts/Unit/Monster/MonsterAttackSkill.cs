using Assets.PixelFantasy.PixelMonsters.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum MonsterAction
{
    BASEATTACK,
    JUMP
}

public class MonsterAttackSkill
{
    Monster monster;
    BattleManager battleManager { get { return monster.battleManager; } }
    MonsterStateMachine stateMachine { get { return monster.stateMachine; } }

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    public MonsterAttackSkill(Monster monster)
    {
        this.monster = monster;
    }

    public void MonsterAttack()
    {
        switch (monster.selectAction)
        {
            case MonsterAction.BASEATTACK:
                monster.StartCoroutine(Attack(1, 1, 2));
                break;
            case MonsterAction.JUMP:
                monster.StartCoroutine(Jump());
                break;
        }
    }

    float GetNormalizedTime(Animator animator, string tag)
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
    bool MoveTowardsMonster(Vector3 target)
    {
        return target != (monster.transform.position =
            Vector3.MoveTowards(monster.transform.position, target, monster.moveAnimSpeed * Time.deltaTime));
    }

    IEnumerator AttackBase(Character target, int damage, int effectId)
    {
        monster.PlayAnim(MonsterAnim.Attack);
        while (1 > GetNormalizedTime(monster.monsterAnimController.animator, "Attack")) { yield return null; }
        monster.PlayAnim(MonsterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkRangeHitSFX(); // 임시 사운드
        battleManager.effectController.SetRepeatEffect(effectId, target.transform.position); // 임시 이펙트
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{monster.monsterName}의 공격!\n{target.characterName}에게 {damage}의 피해!");
    }

    IEnumerator Attack(int damageMultiple, int count, int effectId)
    {
        Character target = battleManager.character;
        Vector3 selectCharacterPosition = target.transform.position + Vector3.right;
        int damage = monster.atk * damageMultiple;

        monster.ChangeAnimState(MonsterAnimState.Running);
        while (MoveTowardsMonster(selectCharacterPosition)) { yield return null; }

        for (int i = 0; i < count; i++)
        {
            yield return AttackBase(target, damage, effectId);
            if (target.IsDead) break;
        }

        while (MoveTowardsMonster(monster.startPosition)) { yield return null; }
        monster.ChangeAnimState(MonsterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
    IEnumerator Jump()
    {
        AudioManager.Instance.PlayMonsterJumpSFX(); // 임시 사운드
        monster.PlayAnim(MonsterAnim.Jump);
        while (1 > GetNormalizedTime(monster.monsterAnimController.animator, "Jump")) { yield return null; }
        monster.PlayAnim(MonsterAnim.Idle);
        yield return waitForEndOfFrame;

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
}
