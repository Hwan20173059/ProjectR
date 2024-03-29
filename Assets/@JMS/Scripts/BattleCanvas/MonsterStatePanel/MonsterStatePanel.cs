using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatePanel : MonoBehaviour
{
    MonsterStateText monsterStateText;

    public void Init()
    {
        monsterStateText = GetComponentInChildren<MonsterStateText>();

        gameObject.SetActive(false);
    }

    public void UpdateMonsterState(Monster selectMonster)
    {
        if (selectMonster == null) return;
        monsterStateText.text = $"{selectMonster.monsterName} {selectMonster.curHP} / {selectMonster.maxHP}";
    }
}
