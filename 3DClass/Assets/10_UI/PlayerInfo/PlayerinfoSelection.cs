using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerinfoSelection
{
    private VisualElement playerHPBar;
    private VisualElement playerEnergyBar;

    public float HPScale
    {
        get => playerHPBar.transform.scale.x;
        set
        {
            playerHPBar.transform.scale = new Vector3(value, 1, 0);
        }
    }

    public float EnergyScale
    {
        get => playerEnergyBar.transform.scale.x;
        set
        {
            playerEnergyBar.transform.scale = new Vector3(value, 1, 0);
        }
    }

    public PlayerinfoSelection(VisualElement root)
    {
        playerHPBar = root.Q("HPRow").Q("bar");
        playerEnergyBar = root.Q("EnergyRow").Q("bar");
    }
}
