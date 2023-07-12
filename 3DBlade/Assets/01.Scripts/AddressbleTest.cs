using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressbleTest : MonoBehaviour
{
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
        Addressables.LoadAssetAsync<GameObject>($"PopupText").Completed += obj =>
        {
            handle = obj;

            popupText = Instantiate(handle.Result, Vector3.zero, Quaternion.identity);

            //Debug.Log(obj);
            //Debug.Log(obj.Result);
        };
    }

    private void UnloadPopupText()
    {
        Destroy(popupText);
        Addressables.Release(handle); // 핸들을 아예 빼서 메모리에서 빼준다
    }
}
