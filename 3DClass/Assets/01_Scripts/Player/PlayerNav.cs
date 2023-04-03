using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNav : MonoBehaviour
{
    private NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos;
            if(GameManager.Instance.GetMouseWorldPosition(out pos))
            {
                _agent.SetDestination(pos);

            }
        }
    }
}
