using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private ItemDropTableSO dropTable;
    private float[] itemWeights;

    [SerializeField][Range(0, 1)] private float dropChance;

    private void Start()
    {
        itemWeights = dropTable.DropList.Select(item => item.Rate).ToArray();
    }

    public void DropItem()
    {
        float dropValue = Random.value;

        if(dropValue < dropChance)
        {
            int idx = GetRandomIndex();
            PoolableMono resource = PoolManager.Instance.Pop(dropTable.DropList[idx].itemPrefab.name);
            resource.transform.position = transform.position;
        }
    }

    private int GetRandomIndex()
    {
        float sum = 0;
        for(int i = 0; i < itemWeights.Length; i++)
        {
            sum += itemWeights[i];
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0;
        for(int i = 0; i < itemWeights.Length; i++)
        {
            if(randomValue >= tempSum && randomValue < tempSum + itemWeights[i])
            {
                return i;
            }
            else
            {
                tempSum = itemWeights[i];
            }
        }
        return 0;
    }
}
