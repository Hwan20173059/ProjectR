using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class GameEndButton : MonoBehaviour
{
    Button button;

    public void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickGameEndButton);
    }

    void OnClickGameEndButton()
    {
        Application.Quit();
    }
}
