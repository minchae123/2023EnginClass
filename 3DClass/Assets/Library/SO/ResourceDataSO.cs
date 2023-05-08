using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[CreateAssetMenu(menuName ="SO/Item/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate;
    public GameObject itemPrefab;

    public ResourceType ResourceType;

    public int minAmount = 1, maxAmount = 5;
    public int GetAmount()
    {
        return Random.Range(minAmount, maxAmount + 1);
    }

    public AudioClip useSound;
    public Color PopupTextColor;
}
