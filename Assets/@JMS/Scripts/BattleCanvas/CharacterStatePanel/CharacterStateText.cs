using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStateText : MonoBehaviour
{
    TextMeshProUGUI characterStateText;
    public string text { get {  return characterStateText.text; } set { characterStateText.text = value; } }

    private void Awake()
    {
        characterStateText = GetComponent<TextMeshProUGUI>();
    }
}
