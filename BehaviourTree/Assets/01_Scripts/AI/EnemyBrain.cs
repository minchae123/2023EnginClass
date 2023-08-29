using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    [SerializeField] protected Transform targetTrm;

    public NavMeshAgent NavAgent { get; private set; }
    [SerializeField] protected Vector3 position;

    protected UIStatusBar statusBar;
    protected Camera mainCam;

    protected virtual void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        statusBar = transform.Find("Status").GetComponent<UIStatusBar>();
        mainCam = Camera.main;
    }

    protected void LateUpdate()
    {
        statusBar.LookToCamera();
    }

    private Coroutine coroutine;

    public void TryToTalk(string text, float timer = 1)
    {
        statusBar.DialogText = text;
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(PanelFade(timer));
    }

    IEnumerator PanelFade(float timer)
    {
        statusBar.IsOn = true;
        yield return new WaitForSeconds(timer);
        statusBar.IsOn = false;
    }
}
