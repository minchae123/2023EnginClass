using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshClimb : MonoBehaviour
{
    [SerializeField] private int offMeshAreaNum = 4;
    [SerializeField] private float climbSpeed = 1.5f;
    private NavMeshAgent navAgnet;

    private void Awake()
    {
        navAgnet = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnClimb());

            yield return StartCoroutine(ClimbOrDescend());
        }
    }

    IEnumerator ClimbOrDescend()
    {
        navAgnet.isStopped = true;
        OffMeshLinkData linkData = navAgnet.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;
        yield return null;
    }

    private bool IsOnClimb()
    {
        if (navAgnet.isOnOffMeshLink)
        {
            OffMeshLinkData linkData = navAgnet.currentOffMeshLinkData;
            
            if(linkData.offMeshLink != null && linkData.offMeshLink.area == offMeshAreaNum)
            {
                return true;
            }
        }
        return false;
    }
}
