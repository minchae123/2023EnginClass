using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshJump : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private int _offMeshAreaNumber = 2;

    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnJump());
            yield return StartCoroutine(JumpTo());
        }
    }

    IEnumerator JumpTo()
    {
        navAgent.isStopped = true;
        OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float curTime = 0;
        float percent = 0;

        float v0 = (end - start).y - gravity; // 초기 속도

        while (percent < 1)
        {
            // 포물선 운동 : 시작위치 + 초기 속도 * 시간 + 중력 * 시간의 제곱
            curTime += Time.deltaTime;
            percent = curTime / jumpTime;

            Vector3 pos = Vector3.Lerp(start, end, percent);
            pos.y = start.y + (v0 * percent) + (gravity * percent * percent);

            transform.position = pos;
            yield return null;
        }

        navAgent.CompleteOffMeshLink();
        navAgent.isStopped = false;
    }

    private bool IsOnJump()
    {
        if (navAgent.isOnOffMeshLink)
        {
            OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;

            if (linkData.offMeshLink != null && linkData.offMeshLink.area == _offMeshAreaNumber)
            {
                return true;
            }
        }
        return false;
    }
}
