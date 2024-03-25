using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterActionBar : MonoBehaviour
{
    public Image actionBar;
    private void Awake()
    {
        actionBar = GetComponent<Image>();
    }
}
