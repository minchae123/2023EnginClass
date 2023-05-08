using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    public UnityEvent<int> OnHeal;

    [SerializeField] private float magneticRadius = 1f, magneticPower = 3f;
    [SerializeField] private LayerMask whatIsResource;
    private List<Resource> collectingList = new List<Resource>();

    private void Update()
    {
        Collider[] resources = Physics.OverlapSphere(transform.position, magneticRadius, whatIsResource);
        foreach(Collider r in resources)
        {
            if(r.TryGetComponent<Resource>(out Resource res))
            {
                collectingList.Add(res);
                res.gameObject.layer = 0;
            }
        }

        for(int i = 0; i < collectingList.Count; i++)
        {
            Resource r = collectingList[i];
            Vector3 nextStep = (transform.position - r.transform.position).normalized * Time.deltaTime * magneticPower;
            r.transform.Translate(nextStep, Space.World);

            if(Vector3.Distance(r.transform.position, transform.position) < 0.3f)
            {
                ConsumeResource(r);
                collectingList.RemoveAt(i);
                i--;
            }
        }
    }

    private void ConsumeResource(Resource r)
    {
        ResourceDataSO so = r.ResourceData;
        switch (so.ResourceType)
        {
            case Core.ResourceType.HealOrb:
                int hpValue = so.GetAmount();
                OnHeal?.Invoke(hpValue);
                r.PickUpResource();
                break;
        }
    }
}
