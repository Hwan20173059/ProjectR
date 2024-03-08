using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public Character character;

    public Sprite sprite;
    public string name;

    public int level;
    public int needExp;
    public int CurrentExp;

    public int maxHp;
    public int currentHp;

    public int attack;

    private void Start()
    {
        playerState = SingletonManager.instance.GetComponentInChildren<PlayerState>();
        Refresh();
    }

    public void Refresh()
    {
        character = playerState.character;

        sprite = character.sprite;
        name = character.name;

        level = character.level;
        needExp = character.needExp;
        CurrentExp = character.currentExp;

        maxHp = character.maxHp;
        currentHp = character.currentHp;

        attack = character.attack;
    }
}
