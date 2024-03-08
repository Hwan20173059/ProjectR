using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerManager playerManager;
    public Character character;

    public SpriteRenderer sprite;
    public string name;

    public int level;
    public int needExp;
    public int CurrentExp;

    public int maxHp;
    public int currentHp;

    public int attack;

    private void Start()
    {
        playerManager = PlayerManager.Instance.GetComponent<PlayerManager>();
        PlayerManager.Instance.playerPrefab = this.gameObject;

        Refresh();
    }
    

    public void Refresh()
    {
        character = playerManager.character;

        sprite.sprite = character.sprite;
        name = character.name;

        level = character.level;
        needExp = character.needExp;
        CurrentExp = character.currentExp;

        maxHp = character.maxHp;
        currentHp = character.currentHp;

        attack = character.attack;
    }
}
