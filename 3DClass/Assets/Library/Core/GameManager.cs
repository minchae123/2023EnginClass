using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PoolingListSO initPoolList;

    private Transform playerTrm;
    public Transform PlayerTrm
    {
        get { if (playerTrm == null)
                playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            return playerTrm;
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        CreatePool();
        CreateTImeController();
    }

    [SerializeField] private LayerMask whatIsGround;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Ray ray = Define.MainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool result = Physics.Raycast(ray, out hit, Define.MainCam.farClipPlane, whatIsGround);

            if (result)
            {
                PoolableMono mono = PoolManager.Instance.Pop("HammerEnemy");

                mono.transform.SetPositionAndRotation(hit.point, Quaternion.identity);
            }
        }
    }

    private void CreateTImeController()
    {
        TimeController.Instance = gameObject.AddComponent<TimeController>();
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        initPoolList.PoolList.ForEach(p =>
        {
            PoolManager.Instance.CreatePool(p.prefab, p.count);
        });
    }
}
