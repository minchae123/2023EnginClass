using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

    }

    private void OnEnable()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        //�̳༮�� UI Object�� . ��� �ֵ��� �ٰ��� �ȴ�.

        Button btn = root.Q<Button>("BtnOpen");
        //Query => ����, ����


        VisualElement popupWindow = root.Q("PopupWindow");

        btn.RegisterCallback<ClickEvent>(e =>
        {
            Time.timeScale = 0;
            popupWindow.AddToClassList("open");
            
        });


        Button closeBtn = root.Q<Button>("BtnClose");
        closeBtn.RegisterCallback<ClickEvent>(e =>
        {
            Time.timeScale = 1;
            popupWindow.RemoveFromClassList("open");
        });
    }

}
