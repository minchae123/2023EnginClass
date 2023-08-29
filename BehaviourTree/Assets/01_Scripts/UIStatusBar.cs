using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIStatusBar : MonoBehaviour
{
    private UIDocument uIDocument;
    private VisualElement panelElement;
    private DialogPanel dialogPanel;
    private BulletUI bulletUI;

    private bool isOn = false;
    public bool IsOn
    {
        get => isOn;
        set
        {
            dialogPanel.Show(value);
            isOn = value;
        }
    }

    public string DialogText
    {
        get => dialogPanel.Text;
        set => dialogPanel.Text = value;
    }

    private Camera mainCam;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        panelElement = uIDocument.rootVisualElement.Q("PanelContainer");
        dialogPanel = new DialogPanel(panelElement, "");

        var bulletContainer = uIDocument.rootVisualElement.Q("BulletContainer");
        bulletUI = new BulletUI(bulletContainer, 8);
    }

    public void LookToCamera()
    {
        Vector3 dir = (transform.position - mainCam.transform.position);
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    public void SetBulletCount(int count )
    {
        bulletUI.BulletCount = count;
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            SetBulletCount(Random.Range(0, 9));
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            dialogPanel.Text = $"Hello {Random.Range(0, 999)}";
        }*/
    }
}
