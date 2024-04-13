using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterHpBar : MonoBehaviour
{
    Monster monster;

    RectTransform monsterMaxHpBar;
    RectTransform monsterCurHpBar;

    private void Update()
    {
        SetHpBarPosition();
    }

    public void Init(Monster monster)
    {
        this.monster = monster;
        monsterMaxHpBar = transform.GetChild(0).GetComponent<RectTransform>();
        monsterCurHpBar = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        if (monster.maxHP > 100)
        {
            float addScale = (monster.maxHP - 100) / 1000;
            monsterMaxHpBar.localScale = new Vector3(monsterMaxHpBar.localScale.x + addScale, monsterMaxHpBar.localScale.y);
        }
    }

    public void SetHpBar()
    {
        monsterCurHpBar.localScale = new Vector3((float)monster.curHP / monster.maxHP, monsterCurHpBar.localScale.y);
    }

    void SetHpBarPosition()
    {
        if (monster != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(monster.transform.position);
            transform.position = new Vector2(screenPos.x, screenPos.y + 130f);
        }
    }
}
