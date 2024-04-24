using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x4OffButton : MonoBehaviour
{
    public Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
