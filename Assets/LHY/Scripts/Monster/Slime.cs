using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public SOMonster soMonster;

    public int level;

    [HideInInspector]
    public int hp;

    [HideInInspector]
    public int attack;

    [HideInInspector]
    public int exp;

    // Start is called before the first frame update
    void Start()
    {
        hp = level * soMonster.hp;
        attack = level * soMonster.attack;
        exp = level * soMonster.exp;
    }
}
