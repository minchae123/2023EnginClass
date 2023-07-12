using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressbleTest : MonoBehaviour
{
    [SerializeField] private AssetReference _ref;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LoadPopupText();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            UnloadPopupText();
        }
    }

    private AsyncOperationHandle<GameObject> handle;
    private GameObject popupText;

    private void LoadPopupText()
    {
        _ref.InstantiateAsync(Vector3.zero, Quaternion.identity).Completed += (obj) =>
        {
            handle = obj;
            popupText = obj.Result;
        };
    }

    private void UnloadPopupText()
    {
        //Addressables.Release(handle); // �ڵ��� �ƿ� ���� �޸𸮿��� ���ش�
        _ref.ReleaseInstance(popupText);
    }
}
