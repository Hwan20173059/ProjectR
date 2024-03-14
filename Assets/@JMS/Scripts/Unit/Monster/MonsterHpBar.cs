using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterHpBar : MonoBehaviour
{
    private Monster monster;

    private RectTransform monsterMaxHpBar;
    private RectTransform monsterCurHpBar;

    public void Init()
    {
        monster = GetComponentInParent<Monster>();

        monsterMaxHpBar = transform.GetChild(0).GetComponent<RectTransform>();
        monsterCurHpBar = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        monsterMaxHpBar.localScale = new Vector3(monster.maxHP / 30f, monsterMaxHpBar.localScale.y);
    }

    public void SetHpBar()
    {
        monsterCurHpBar.localScale = new Vector3((float)monster.curHP / monster.maxHP, monsterCurHpBar.localScale.y);
    }
}
