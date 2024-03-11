using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectSlot : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject[] characterSlot;
    public CharacterManager characterManager;

    private void Awake()
    {
        for (int i = 0; i < characterSlot.Length; i++)
        {
            CharacterSlot _charactorSlot = characterSlot[i].GetComponent<CharacterSlot>();

            _charactorSlot.characterSO = characterManager.characterList[i];
        }
    }

    private void Start()
    {
        playerManager = PlayerManager.Instance.GetComponent<PlayerManager>();
    }

    public void SelectCharacter(int index)
    {
        for (int i = 0; i < characterSlot.Length; i++)
            characterSlot[i].GetComponent<CharacterSlot>().CharacterUnSelect();

        characterSlot[index].GetComponent<CharacterSlot>().CharacterSelect();
        playerManager.selectedCharacter = characterSlot[index].GetComponent<CharacterSlot>().characterSO;

        playerManager.ReFreshPlayer();
    }
}
