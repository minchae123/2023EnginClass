using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIDocument _uiDocument;
    private VisualElement _root;

    private EnemyHPBar _enemyHPBar;
    private PlayerInfoSection _playerInfo;

    [SerializeField]
    private float _enemyBarTimer = 4f, _currentEnemyBarTimer = 0f;

    private EnemyHealth _subscribedEnemy = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple UIManager is running");
        }
        Instance = this;

        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _uiDocument.rootVisualElement;
        VisualElement _hpBarRoot = _root.Q("HPBarRect");
        _enemyHPBar = new EnemyHPBar(_hpBarRoot); //�� Ŭ������ HPbar�� 1:1�� ������ ���ε� �ȴ�.

        VisualElement playerInfoRoot = _root.Q<VisualElement>("ProfileBox");
        _playerInfo = new PlayerInfoSection(playerInfoRoot);

        SetUpPlayer();
    }

    private void SetUpPlayer()
    {
        AgentHealth health = GameManager.Instance.PlayerTrm.GetComponent<AgentHealth>();
        health.OnHealthChanged += (current, max) =>
        {
            _playerInfo.HPScale = (float)current / max;
        };
    }

    public void Subscribe(EnemyHealth health)
    {
        if(_currentEnemyBarTimer <= 0)
        {
            _enemyHPBar.ShowBar(true);
        }

        if(_subscribedEnemy != health)
        {
            if(_subscribedEnemy != null)
            {
                _subscribedEnemy.OnHealthChanged -= UpdateEnemyHPData;
            }
            _subscribedEnemy = health;
            _subscribedEnemy.OnHealthChanged += UpdateEnemyHPData;

            _enemyHPBar.EnemyName = health.gameObject.name; //���� ���� �̸��� �ִٸ� 
            _enemyHPBar.MaxHP = _subscribedEnemy.MaxHP;
        }
    }

    private void UpdateEnemyHPData(int current, int max)
    {
        _enemyHPBar.HP = current;
        _currentEnemyBarTimer = _enemyBarTimer;
    }

    private void Update()
    {
        if(_currentEnemyBarTimer > 0)
        {
            _currentEnemyBarTimer -= Time.deltaTime;
            if(_currentEnemyBarTimer <= 0)
            {
                _enemyHPBar.ShowBar(false);
            }
        }
    }
}
