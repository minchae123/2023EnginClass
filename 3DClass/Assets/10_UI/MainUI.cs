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

        Button btn = root.Q<Button>("BtnOpen");
        btn.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("버튼 클릭");
        });
    }
}
