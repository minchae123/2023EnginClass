using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletUI 
{
    private int currentCnt;
    private int maxCnt;
    private List<VisualElement> bulletList;

    public int BulletCount
    {
        get => currentCnt;
        set
        {
            currentCnt = Mathf.Clamp(value, 0, maxCnt);
            DrawBullet();
        }
    }

    private void DrawBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if(i < currentCnt - 1)
            {
                bulletList[i].AddToClassList("on");
            }
            else
            {
                bulletList[i].RemoveFromClassList("on");
            }
        }
    }

    public BulletUI(VisualElement root, int maxCount )
    {
        maxCnt = maxCount;
        bulletList = root.Query<VisualElement>(className: "bullet").ToList();
    }
}
