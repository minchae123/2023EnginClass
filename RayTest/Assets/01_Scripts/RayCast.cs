using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private float maxDis = 5;
    public LayerMask layerMask;
    private Rigidbody rb;

    private void Update()
    {
        RaycastHit hit;

        bool isHit = Physics.BoxCast(
            transform.position,
            transform.lossyScale * 0.5f,
            transform.forward,
            out hit,
            transform.rotation,
            maxDis, layerMask);

        if (isHit)
        {
            if(Input.GetMouseButtonDown(0))
                 hit.collider.GetComponent<Rigidbody>().AddForce(-hit.normal * 5);
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        bool isHit = Physics.BoxCast(
            transform.position,
            transform.lossyScale * 0.5f,
            transform.forward,
            out hit,
            transform.rotation,
            maxDis, layerMask);

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
