using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHPBar : MonoBehaviour
{
    private VisualElement barParent;
    private VisualElement bar;
    private Label hpLabel;
    private Label nameLabel;

    private int curHP;
    public int HP
    {
        set
        {
            curHP = value;
            UpdateHPText();
        }
    }

    private int maxHP;
    public int MaxHP
    {
        set
        {
            maxHP = value;
            UpdateHPText();
        }
    }

    public string EnemyName
    {
        set
        {
            nameLabel.text = $"{value}";
        }
    }
    
    private void UpdateHPText()
    {
        hpLabel.text = $"{curHP} / {maxHP}";
        bar.transform.scale = new Vector3((float)curHP / maxHP, 1, 0);
    }

    public void ShowBar(bool value)
    {
        if (value)
        {
            barParent.AddToClassList("on");
        }
        else
        {
            barParent.RemoveFromClassList("on");
        }
    }

    public EnemyHPBar(VisualElement root)
    {
        barParent = root;
        bar = barParent.Q<VisualElement>("Bar");
        nameLabel = barParent.Q<Label>("EnemyName");
        hpLabel = barParent.Q<Label>("HPText");
    }
}
