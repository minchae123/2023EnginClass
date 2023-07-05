using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncAwait : MonoBehaviour
{
    private void Start()
    {
        if(Thread.CurrentThread.Name == null)
        {
            Thread.CurrentThread.Name = "MainThread";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartTask();
            //Task.Run(() => MyJob1());
            //StartCoroutine(MyJob());
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("살아있다");
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"num : {num}");
        }
    }

    private async void StartTask()
    {
        num = 0;
        Task t1 = Task.Run(() => MyJobA());
        Task t2 = Task.Run(() => MyJobB());

        await Task.WhenAll(new[] {t1, t2 });

        //await t1;
        //await t2;
        Debug.Log("Job Complete");
    }

    /*IEnumerator MyJob()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(Thread.CurrentThread.Name);
        Debug.Log("Job Complete");
    }*/

    /*private void MyJob1()
    {
        Thread.Sleep(3000);
        Debug.Log(Thread.CurrentThread.Name);
        Debug.Log("Job Complete");
    }*/

    private int num = 0;

    private object obj = new object();

    private void MyJobA()
    {
        for (int i = 0; i < 99999999; i++)
        {
            lock(obj)
            {
                num++;
            }
        }
    }

    private void MyJobB()
    {
        for (int i = 0; i < 99999999; i++)
        {
            lock(obj)
            {
                num--;
            }
        }
    }
}
