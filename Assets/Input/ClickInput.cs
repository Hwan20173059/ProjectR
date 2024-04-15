//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/ClickInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ClickInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ClickInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ClickInput"",
    ""maps"": [
        {
            ""name"": ""Click"",
            ""id"": ""2be886fc-85b0-4d6c-95d5-14de178fba8a"",
            ""actions"": [
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""c6510d8e-d115-4b83-8474-344c132307c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""56818ad7-fbbf-4777-ba5b-970ecc401297"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPos"",
                    ""type"": ""Value"",
                    ""id"": ""09d36ed0-cd28-4414-88ab-1cb0e094ed2c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""63e20ad0-d0d2-42f3-89de-0e1e70e67091"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b8929746-40ac-4ac7-b2dc-d59d99fff2eb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40436ba3-be50-4031-9394-e481eb9a6b21"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38b74b8f-fce8-43a2-95e9-eb676536743a"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""931898b3-0bea-4089-b802-8eb8f45a8c91"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Click
        m_Click = asset.FindActionMap("Click", throwIfNotFound: true);
        m_Click_MouseClick = m_Click.FindAction("MouseClick", throwIfNotFound: true);
        m_Click_MousePos = m_Click.FindAction("MousePos", throwIfNotFound: true);
        m_Click_TouchPos = m_Click.FindAction("TouchPos", throwIfNotFound: true);
        m_Click_TouchPress = m_Click.FindAction("TouchPress", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Click
    private readonly InputActionMap m_Click;
    private List<IClickActions> m_ClickActionsCallbackInterfaces = new List<IClickActions>();
    private readonly InputAction m_Click_MouseClick;
    private readonly InputAction m_Click_MousePos;
    private readonly InputAction m_Click_TouchPos;
    private readonly InputAction m_Click_TouchPress;
    public struct ClickActions
    {
        private @ClickInput m_Wrapper;
        public ClickActions(@ClickInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseClick => m_Wrapper.m_Click_MouseClick;
        public InputAction @MousePos => m_Wrapper.m_Click_MousePos;
        public InputAction @TouchPos => m_Wrapper.m_Click_TouchPos;
        public InputAction @TouchPress => m_Wrapper.m_Click_TouchPress;
        public InputActionMap Get() { return m_Wrapper.m_Click; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ClickActions set) { return set.Get(); }
        public void AddCallbacks(IClickActions instance)
        {
            if (instance == null || m_Wrapper.m_ClickActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ClickActionsCallbackInterfaces.Add(instance);
            @MouseClick.started += instance.OnMouseClick;
            @MouseClick.performed += instance.OnMouseClick;
            @MouseClick.canceled += instance.OnMouseClick;
            @MousePos.started += instance.OnMousePos;
            @MousePos.performed += instance.OnMousePos;
            @MousePos.canceled += instance.OnMousePos;
            @TouchPos.started += instance.OnTouchPos;
            @TouchPos.performed += instance.OnTouchPos;
            @TouchPos.canceled += instance.OnTouchPos;
            @TouchPress.started += instance.OnTouchPress;
            @TouchPress.performed += instance.OnTouchPress;
            @TouchPress.canceled += instance.OnTouchPress;
        }

        private void UnregisterCallbacks(IClickActions instance)
        {
            @MouseClick.started -= instance.OnMouseClick;
            @MouseClick.performed -= instance.OnMouseClick;
            @MouseClick.canceled -= instance.OnMouseClick;
            @MousePos.started -= instance.OnMousePos;
            @MousePos.performed -= instance.OnMousePos;
            @MousePos.canceled -= instance.OnMousePos;
            @TouchPos.started -= instance.OnTouchPos;
            @TouchPos.performed -= instance.OnTouchPos;
            @TouchPos.canceled -= instance.OnTouchPos;
            @TouchPress.started -= instance.OnTouchPress;
            @TouchPress.performed -= instance.OnTouchPress;
            @TouchPress.canceled -= instance.OnTouchPress;
        }

        public void RemoveCallbacks(IClickActions instance)
        {
            if (m_Wrapper.m_ClickActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IClickActions instance)
        {
            foreach (var item in m_Wrapper.m_ClickActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ClickActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ClickActions @Click => new ClickActions(this);
    public interface IClickActions
    {
        void OnMouseClick(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnTouchPos(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
    }
}
