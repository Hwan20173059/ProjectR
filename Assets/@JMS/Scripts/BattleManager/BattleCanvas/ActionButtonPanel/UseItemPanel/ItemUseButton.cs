using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseButton : MonoBehaviour
{
    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
