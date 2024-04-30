using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterActionBar : MonoBehaviour
{
    Image actionBar;

    public void Init()
    {
        actionBar = GetComponent<Image>();
    }

    public void UpdateActionBar(Character character)
    {
        if (character != null)
            actionBar.transform.localScale =
                new Vector3(Mathf.Clamp(character.curCoolTime / character.maxCoolTime, 0, 1), 1, 1);
    }
}
