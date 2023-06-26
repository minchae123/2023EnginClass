using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    [SerializeField] private Transform startTrm, endTrm, startControlTrm, endControlTrm;

    private LineRenderer lineRenderer;
    List<Vector3> pointList = new List<Vector3>();

    private Transform coinTrm;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        coinTrm = transform.Find("CoinTemplate");
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

        if(Input.GetKeyDown(KeyCode.O)) // 코인 생성기
        {
            for (int i = 0; i < 20; i++)
            {
                Vector2 pos = Random.insideUnitCircle * 2;
                Vector3 targetPos = transform.position + new Vector3(pos.x, 0, pos.y);
                
                Transform trm = Instantiate(coinTrm, transform);
                
                trm.gameObject.SetActive(true);
                trm.DOJump(targetPos, 4f, 1, 1.2f).SetEase(Ease.InQuad);
            }
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