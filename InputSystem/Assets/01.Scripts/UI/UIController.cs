using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private UIDocument uIDocument;

    [SerializeField] private PlayerInput playerInput;
    private Dictionary<string, InputAction> inputMap;

    private VisualElement menu;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
    }

    private void Start()
    {
        inputMap = new Dictionary<string, InputAction>();
        inputMap.Add("Fire", playerInput.InputAction.Player.Fire);
        inputMap.Add("Jump", playerInput.InputAction.Player.Jump);
        inputMap.Add("Movement", playerInput.InputAction.Player.Movement);
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OpenWindow();
        }
    }

    private void OpenWindow()
    {
        menu.AddToClassList("open");
        playerInput.InputAction.Player.Disable();
    }

    private void CloseWindow()
    {
        menu.RemoveFromClassList("open");
        playerInput.InputAction?.Player.Enable();
    }

    private void HandleKeyBindClick(ClickEvent evt)
    {
        var label = evt.target as UILabelWithData;
        if (label == null) return;

        var oldText = label.text;
        label.text = "Listening...";

        if(inputMap.TryGetValue(label.KeyData, out InputAction action))
        {
            var queue = action.PerformInteractiveRebinding();

            if(label.KeyData != "Fire")
            {
                queue = queue.WithControlsExcluding("Mouse");
            }
            queue.WithTargetBinding(label.IndexData)
                .WithCancelingThrough("<keyboard>/escape")
                .OnComplete(op =>
                {
                    label.text = op.selectedControl.name;
                    op.Dispose();
                })
                .OnCancel(op =>
                {
                    label.text = oldText;
                })
                .Start();
        }
        else
        {
            label.text = oldText;
        }
    }

    private void OnEnable()
    {
        menu = uIDocument.rootVisualElement.Q("MenuBox");

        menu.RegisterCallback<ClickEvent>(HandleKeyBindClick);

        uIDocument.rootVisualElement.Q<Button>("BtnCancel").RegisterCallback<ClickEvent>(e =>
        {
            CloseWindow();
        });

        uIDocument.rootVisualElement.Q<Button>("BtnSave").RegisterCallback<ClickEvent>(e =>
        {
            CloseWindow();
        });

        CloseWindow();
    }
}
