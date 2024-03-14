using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHpBar : MonoBehaviour
{
    private Monster monster;

    private Transform monsterMaxHpBar;
    private Transform monsterCurHpBar;

    private float hpBarDefaultScale;

    public void Init()
    {
        monster = GetComponentInParent<Monster>();

        monsterMaxHpBar = transform.GetChild(0).transform;
        monsterCurHpBar = transform.GetChild(1).transform;

        monsterMaxHpBar.localScale = new Vector3(monster.maxHP / 30f, monsterMaxHpBar.localScale.y);
        monsterCurHpBar.localScale = monsterMaxHpBar.localScale;
        hpBarDefaultScale = monster.maxHP / 30f;
    }

    public void SetHpBar()
    {
        monsterCurHpBar.localScale = new Vector3(monster.curHP * hpBarDefaultScale / monster.maxHP, monsterCurHpBar.localScale.y);
    }
}
