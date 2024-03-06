using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Character character;
    public Equip[] equip;

    public Transform playerArea;
    public GameObject characterPrefab;

    private void Start()
    {
        Instantiate(characterPrefab,playerArea);
    }
}
