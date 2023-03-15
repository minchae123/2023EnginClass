using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private float maxDis = 5;
    public LayerMask layerMask;
    private void OnDrawGizmos()
    {
        RaycastHit hit;

        /*bool isHit = Physics.Raycast(
            transform.position, 
            transform.forward, 
            out hit, 
            maxDis, layerMask);*/

        bool isHit = Physics.BoxCast(
            transform.position,
            transform.lossyScale * 0.5f,
            transform.forward,
            out hit,
            transform.rotation,
            maxDis);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * maxDis);
        }
    }
}
