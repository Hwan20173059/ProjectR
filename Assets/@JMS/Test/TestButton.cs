using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using UnityEngine.UI;
using TMPro;


public class TestButton : MonoBehaviour
{
    public TestCharacterAnimController animController;
    public TextMeshProUGUI playAnimText;
    public TextMeshProUGUI animStateText;

    public TestCharacterAnims anim = TestCharacterAnims.Attack;
    public TestCharacterAnimState animState = TestCharacterAnimState.Ready;

    public TestMonsterAnimController monsterAnimController;
    public TextMeshProUGUI playMonsterAnimText;
    public TextMeshProUGUI monsterAnimStateText;

    public TestMonsterAnims monsterAnim = TestMonsterAnims.Attack;
    public TestMonsterAnimState monsterAnimState = TestMonsterAnimState.Ready;

    public void ChangeAnim()
    {
        ++anim;
        if ((int)anim > 8) anim = 0;
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
        if ((int)monsterAnim > 2) monsterAnim = 0;
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
