using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatePanel : MonoBehaviour
{
    MonsterStateText monsterStateText;

    public void Init()
    {
        monsterStateText = GetComponentInChildren<MonsterStateText>();
        monsterStateText.Init();

        gameObject.SetActive(false);
    }

    public void UpdateMonsterState(Monster selectMonster)
    {
        monsterStateText.UpdateMonsterState(selectMonster);
    }
}
