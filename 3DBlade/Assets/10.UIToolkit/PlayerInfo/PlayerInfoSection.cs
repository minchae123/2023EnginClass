using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//������ ���ε�
public class PlayerInfoSection
{
    private VisualElement _playerHPBar;
    private VisualElement _playerEnergyBar;

    public float HPScale
    {
        get => _playerHPBar.transform.scale.x;
        set
        {
            _playerHPBar.transform.scale = new Vector3(value, 1, 0);
        }
    }

    public float EnergyScale
    {
        get => _playerEnergyBar.transform.scale.x;
        set
        {
            _playerEnergyBar.transform.scale = new Vector3(value, 1, 0);
        }
    }

    public PlayerInfoSection(VisualElement root)
    {
        _playerHPBar = root.Q("HPRow").Q("Bar");
        _playerEnergyBar = root.Q("EnergyRow").Q("Bar");
    }
}
