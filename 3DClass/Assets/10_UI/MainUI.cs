using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = uiDocument.rootVisualElement;

        Button btnO = root.Q<Button>("BtnOpen");

        VisualElement popupWindow = root.Q("PopupWindow");

        btnO.RegisterCallback<ClickEvent>(e =>
        {
            Time.timeScale = 0;
            popupWindow.AddToClassList("open");
        });

        Button btnC = root.Q<Button>("BtnClose");
        btnC.RegisterCallback<ClickEvent>(e =>
        {
            Time.timeScale = 1;
            popupWindow.RemoveFromClassList("open");
        });
    }
}
