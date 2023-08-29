using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogPanel
{
    private Label label;
    public string Text
    {
        get => label.text;
        set => label.text = value;
    }

    public DialogPanel(VisualElement root, string msg)
    {
        label = root.Q<Label>("MessageLabel");
    }

    public void Show(bool value)
    {

    }
}
