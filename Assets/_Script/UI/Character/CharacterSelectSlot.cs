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

            _charactorSlot.characterData = characterManager.characterList[i];
        }
    }

    private void Start()
    {
        playerManager = PlayerManager.Instance.GetComponent<PlayerManager>();
    }

    public void SelectCharacter(int index)
    {
        characterManager.SelectCharacter(index);
        playerManager.selectedCharacter = characterManager.selectedCharacter;

        for (int i = 0; i < characterSlot.Length; i++) 
            characterSlot[i].GetComponent<CharacterSlot>().Refresh();

        playerManager.ReFreshPlayer();
    }
}
