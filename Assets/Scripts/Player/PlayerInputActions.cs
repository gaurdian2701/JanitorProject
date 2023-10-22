//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Scripts/Player/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Base"",
            ""id"": ""bd89c71e-a0f6-4629-a9b6-bf8259c90877"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""afc44c05-8f7d-4ed5-96d3-6058d9b16180"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""20272730-843e-489b-8a66-973ba88c2e64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Suck"",
                    ""type"": ""Button"",
                    ""id"": ""ebe2a4b2-2e08-468f-becc-3fdcc7fb6eaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b07b04cc-fb9d-44a4-a764-6cd4a0d50798"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""5dd2b24b-8e0e-4884-ae15-b4f4743dfe57"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d818d913-a4f8-4795-ac4e-08bd8e0c8135"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""61e2ba00-bc32-4b9e-a157-86d7e0f74cf0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e14b282a-7fa7-49e2-873d-0c8020393fde"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09a278d5-e4f6-4843-8cf0-527435a49570"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Suck"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0ccae7d-7812-40fd-8024-bce63b279248"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Base
        m_Base = asset.FindActionMap("Base", throwIfNotFound: true);
        m_Base_Move = m_Base.FindAction("Move", throwIfNotFound: true);
        m_Base_Attack = m_Base.FindAction("Attack", throwIfNotFound: true);
        m_Base_Suck = m_Base.FindAction("Suck", throwIfNotFound: true);
        m_Base_Jump = m_Base.FindAction("Jump", throwIfNotFound: true);
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

    // Base
    private readonly InputActionMap m_Base;
    private List<IBaseActions> m_BaseActionsCallbackInterfaces = new List<IBaseActions>();
    private readonly InputAction m_Base_Move;
    private readonly InputAction m_Base_Attack;
    private readonly InputAction m_Base_Suck;
    private readonly InputAction m_Base_Jump;
    public struct BaseActions
    {
        private @PlayerInputActions m_Wrapper;
        public BaseActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Base_Move;
        public InputAction @Attack => m_Wrapper.m_Base_Attack;
        public InputAction @Suck => m_Wrapper.m_Base_Suck;
        public InputAction @Jump => m_Wrapper.m_Base_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Base; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseActions set) { return set.Get(); }
        public void AddCallbacks(IBaseActions instance)
        {
            if (instance == null || m_Wrapper.m_BaseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BaseActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Suck.started += instance.OnSuck;
            @Suck.performed += instance.OnSuck;
            @Suck.canceled += instance.OnSuck;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IBaseActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Suck.started -= instance.OnSuck;
            @Suck.performed -= instance.OnSuck;
            @Suck.canceled -= instance.OnSuck;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IBaseActions instance)
        {
            if (m_Wrapper.m_BaseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBaseActions instance)
        {
            foreach (var item in m_Wrapper.m_BaseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BaseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BaseActions @Base => new BaseActions(this);
    public interface IBaseActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnSuck(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
