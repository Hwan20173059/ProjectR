using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterActionState : CharacterBaseState
{
    public CharacterActionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateUpdate("������");

        if (battleManager.rouletteResult == RouletteResult.Triple)
        {
            ItemSkill(battleManager.rouletteEquip[0].data.id);
        }
        else
        {
            character.StartCoroutine(BaseAttack());
        }
    }

    void ItemSkill(int itemID)
    {
        switch (itemID)
        {
            case 0: // ������ �ָ�
                character.StartCoroutine(BaseAttack());
                break;
            case 1: // �Ҳ��� ���
                character.StartCoroutine(DoubleAttack());
                break;
            case 2: // ������ ��
                character.StartCoroutine(EatBread());
                break;
            case 3: // �ٴ��� ����
                character.StartCoroutine(HorizontalAttack());
                break;
            case 4: // �¸��� ��
                character.StartCoroutine(AllAttack());
                break;
            default:
                character.StartCoroutine(BaseAttack());
                break;
        }
    }

    IEnumerator Attack(Monster target, int damage)
    {
        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        battleManager.battleCanvas.SetDurationEffect(0, target.transform.position); // �ӽ� ����Ʈ
        int prevHp = target.curHP;
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� ����!\n{target.monsterName}���� {prevHp - target.curHP}�� ����!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);
    }

    IEnumerator BaseAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(target.startPosition.x -1f, target.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return character.StartCoroutine(Attack(target, damage));

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttack()
    {
        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            battleManager.monsters[i].ChangeHP(-damage);
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� ��ü ����!\n���͵鿡�� {damage}�� ������ ����!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleAttack()
    {
        if(battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = new Vector3(target.startPosition.x - 1f, target.startPosition.y);
            Vector3 nextMonsterPosition = new Vector3(nextTarget.startPosition.x - 1f, nextTarget.startPosition.y);
            
            int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(target, damage));

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(nextTarget, damage));

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(BaseAttack());
        }
    }

    IEnumerator EatBread()
    {
        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Jump");
        int prevHp = character.curHP;
        character.ChangeHP(battleManager.rouletteEquip[0].data.singleValue);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� {character.curHP - prevHp}�� ü���� ȸ��!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Jump"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        character.StartCoroutine(BaseAttack());
    }

    IEnumerator HorizontalAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(target.startPosition.x - 1f, target.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector2.right, 4f);
        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� ���� ����!\n���͵鿡�� {damage}�� ������ ����!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    private bool MoveTowardsCharacter(Vector3 target)
    {
        return target != (character.transform.position =
            Vector3.MoveTowards(character.transform.position, target, character.moveAnimSpeed * Time.deltaTime));
    }
    private bool IsAnimationEnd(float animNormalizedTime)
    {
        return animNormalizedTime >= 1f;
    }

    public override void Exit()
    {
        base.Exit();
        character.ReduceBuffDuration();
    }
}
