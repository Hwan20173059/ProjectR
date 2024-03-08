using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectSlot : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject[] characterSlot;
    public CharacterManager characterManager;

    private void Awake()
    {
        for (int i = 0; i < characterSlot.Length; i++)
        {
            CharacterSlot _charactorSlot = characterSlot[i].GetComponent<CharacterSlot>();

            _charactorSlot.character = characterManager.characterList[i];
        }
    }

    private void Start()
    {
        playerState = PlayerState.Instance.GetComponent<PlayerState>();
    }

    public void SelectCharacter(int index)
    {
        for (int i = 0; i < characterSlot.Length; i++)
            characterSlot[i].GetComponent<CharacterSlot>().CharacterUnSelect();

        characterSlot[index].GetComponent<CharacterSlot>().CharacterSelect();
        playerState.character = characterSlot[index].GetComponent<CharacterSlot>().character;

        playerState.SpawnPlayer();
    }
}
