using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
}
