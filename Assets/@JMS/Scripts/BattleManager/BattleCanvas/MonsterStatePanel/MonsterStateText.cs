using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterStateText : MonoBehaviour
{
    TextMeshProUGUI monsterStateText;

    public void Init()
    {
        monsterStateText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateMonsterState(Monster selectMonster)
    {
        if (selectMonster == null) return;

        monsterStateText.text = $"{selectMonster.monsterName} {selectMonster.curHP} / {selectMonster.maxHP}";
    }
}
