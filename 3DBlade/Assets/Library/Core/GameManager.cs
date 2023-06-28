using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _initPoolList;

    [SerializeField]
    private GameObject UIGameObject;

    private Transform _playerTrm;
    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            return _playerTrm;
        }
    }

    private void ActiveUI()
    {
        UIGameObject.SetActive(true);
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        CreatePool();
        CreateTimeController();
        CreateCameraManager();
        ActiveUI();
    }

    private void CreateCameraManager()
    {
        GameObject camManager = new GameObject("CameraManager");
        camManager.transform.parent = transform;
        CameraManager.Instance = camManager.AddComponent<CameraManager>();
        Transform camTrm = GameObject.Find("FollowCam").transform;
        CameraManager.Instance.Init(camTrm);
    }

    private void CreateTimeController()
    {
        TimeController.Instance = gameObject.AddComponent<TimeController>();
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _initPoolList.PoolList.ForEach(p =>
        {
            PoolManager.Instance.CreatePool(p.Prefab, p.Count);
        });
    }


    //����� �ڵ�
    //[SerializeField]
    //private LayerMask _whatIsGround;
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Ray ray = Define.MainCam.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        bool result = Physics.Raycast(ray, out hit, Define.MainCam.farClipPlane, _whatIsGround);
            
    //        if(result)
    //        {
    //            PoolableMono mono = PoolManager.Instance.Pop("HammerEnemy");

    //            mono.transform.SetPositionAndRotation(hit.point, Quaternion.identity);
    //        }
    //    }
    //}
}
