using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using UnityEngine.UI;
using TMPro;


public class TestButton : MonoBehaviour
{
    public CharacterAnimController animController;
    public TextMeshProUGUI playAnimText;
    public TextMeshProUGUI animStateText;

    public CharacterAnim anim = CharacterAnim.Attack;
    public CharacterAnimState animState = CharacterAnimState.Ready;

    public MonsterAnimController monsterAnimController;
    public TextMeshProUGUI playMonsterAnimText;
    public TextMeshProUGUI monsterAnimStateText;

    public MonsterAnim monsterAnim = MonsterAnim.Attack;
    public MonsterAnimState monsterAnimState = MonsterAnimState.Ready;

    public void ChangeAnim()
    {
        ++anim;
        if ((int)anim > 10) anim = 0;
        playAnimText.text = anim.ToString();
    }

    public void PlayAnim()
    {
        animController.PlayAnim(anim);
    }

    public void ChangeAnimState()
    {
        ++animState;
        if ((int)animState > 9) animState = 0;
        animController.ChangeAnimState(animState);
        animStateText.text = animState.ToString();
    }

    public void ChangeMonsterAnim()
    {
        ++monsterAnim;
        if ((int)monsterAnim > 4) monsterAnim = 0;
        playMonsterAnimText.text = monsterAnim.ToString();
    }

    public void PlayMonsterAnim()
    {
        monsterAnimController.PlayAnim(monsterAnim);
    }

    public void ChangeMonsterAnimState()
    {
        ++monsterAnimState;
        if ((int)monsterAnimState > 3) monsterAnimState = 0;
        monsterAnimController.ChangeAnimState(monsterAnimState);
        monsterAnimStateText.text = monsterAnimState.ToString();
    }
}
