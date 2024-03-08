using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectSlot : MonoBehaviour
{
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
}
