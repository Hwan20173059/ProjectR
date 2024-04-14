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
        else if (battleManager.rouletteResult == RouletteResult.Cheat)
        {
            ItemSkill(battleManager.cheatItemId);
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
            case 1: // ü����(��)
                character.StartCoroutine(DoubleAttack());
                break;
            case 2: // ��������
                character.StartCoroutine(JumpHeal());
                break;
            case 3: // ���� ��
                character.StartCoroutine(HorizontalAttack());
                break;
            case 4: // Ǫ�� ���
                character.StartCoroutine(AllAttack());
                break;
            default:
                character.StartCoroutine(BaseAttack());
                break;
        }
    }

    IEnumerator Attack(Monster target, int damage)
    {
        battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position); // �ӽ� ����Ʈ
        int prevHp = target.curHP;
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� ����!\n{target.monsterName}���� {prevHp - target.curHP}�� ����!");
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
    }

    IEnumerator BaseAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(target.startPosition.x - 1f, target.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return character.StartCoroutine(Attack(target, damage));

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttack()
    {
        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.monsters[i].ChangeHP(-damage);
                battleManager.battleCanvas.SetRepeatEffect(0, battleManager.monsters[i].transform.position); // �ӽ� ����Ʈ
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� ��ü ����!\n���͵鿡�� {damage}�� ������ ����!");

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleAttack()
    {
        if (battleManager.AliveMonsterCount() > 1)
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

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(target, damage));

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(nextTarget, damage));

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(BaseAttack());
        }
    }

    IEnumerator JumpHeal()
    {
        int prevHp = character.curHP;
        character.ChangeHP(battleManager.rouletteEquip[0].data.tripleValue);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� {character.curHP - prevHp}�� ü���� ȸ��!");

        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        character.StartCoroutine(BaseAttack());
    }

    IEnumerator HorizontalAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(target.startPosition.x - 1f, target.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector2.right, 4f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}�� ���� ����!\n���͵鿡�� {damage}�� ������ ����!");

        battleManager.battleCanvas.SetMoveEffect(0, target.transform.position); // �ӽ� ����Ʈ
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    private bool MoveTowardsCharacter(Vector3 target)
    {
        return target != (character.transform.position =
            Vector3.MoveTowards(character.transform.position, target, character.moveAnimSpeed * Time.deltaTime));
    }

    public override void Exit()
    {
        base.Exit();
        character.ReduceBuffDuration();
    }
}
