using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    [SerializeField] private Transform startTrm, endTrm, startControlTrm, endControlTrm;

    private LineRenderer lineRenderer;
    List<Vector3> pointList = new List<Vector3>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DOCurve.CubicBezier.GetSegmentPointCloud(pointList, startTrm.position, startControlTrm.position, endTrm.position, endControlTrm.position, 20);
            
            lineRenderer.positionCount = pointList.Count;
            lineRenderer.SetPositions(pointList.ToArray());

            StartCoroutine(MoveCube(2));
        }
    }

    IEnumerator MoveCube(float time)
    {
        float animateTime = time / pointList.Count;
        for (int i = 0; i < pointList.Count; i++) 
        {
            yield return new WaitForSeconds(animateTime);
            transform.position = pointList[i];
        }
    }
}