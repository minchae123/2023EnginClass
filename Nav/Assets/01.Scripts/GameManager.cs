using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private LayerMask _whatIsBase;

    private NavMeshSurface navSurface;

    private Camera _mainCam;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        navSurface = GetComponent<NavMeshSurface>();
    }

    private void ReBakeSurface()
    {
        Debug.Log("������ũ");
        navSurface.BuildNavMesh();
    }

    private void Start()
    {
        _mainCam = Camera.main; //���� ī�޶� ĳ��
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
            if(result)
            {
                BaseBlock block = hit.collider.GetComponent<BaseBlock>();

                block?.ClickBaseBlock();
                ReBakeSurface();
            }
        }
    }

    public bool GetMouseWorldPosition(out Vector3 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
        if (result)
        {
            pos = hit.point;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }
}
