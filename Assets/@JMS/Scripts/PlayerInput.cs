using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public ClickInput InputAction { get; private set; }
    public ClickInput.ClickActions ClickActions { get; private set; }

    private void Awake()
    {
        InputAction = new ClickInput();
        ClickActions = InputAction.Click;
    }

    private void OnEnable()
    {
        InputAction.Click.Enable();
    }
    private void OnDisable()
    {
        InputAction.Click.Disable();
    }
}
