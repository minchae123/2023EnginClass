using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Resource : PoolableMono
{
    [SerializeField] private ResourceDataSO resourceData;
    public ResourceDataSO ResourceData => resourceData;

    private AudioSource audioSource;
    private Collider col;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = resourceData.useSound;
        col = GetComponent<Collider>();
    }

    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        if(audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + 0.3f);
        }
        else
        {
            yield return null;
        }

        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        gameObject.layer = LayerMask.NameToLayer("Resource");
        col.enabled = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
