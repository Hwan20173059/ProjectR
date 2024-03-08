using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleUIController : MonoBehaviour
{
    public TMP_Text BattleText;
    public GameObject VoidPanel;
    public GameObject AttackCancelPanel;

    BattleManager battleManager;
    private void Awake()
    {
        battleManager = GetComponentInParent<BattleManager>();
    }

    public void ToAttack(bool value)
    {
        battleManager.selectingMonster = value;
    }
}
