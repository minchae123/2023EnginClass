using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIDocument uiDocument;
    private VisualElement root;

    private EnemyHPBar enemyHPBar;

    [SerializeField] private float enemyBarTimer = 4f, curEnemyBarTimer = 0f;

    private EnemyHealth subscribedEnemy = null;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("UIManager error");
        }
        Instance = this;
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        root = uiDocument.rootVisualElement;
        VisualElement hpBarRoot = root.Q("HPBarRect");
        enemyHPBar = new EnemyHPBar(hpBarRoot);
    }

    public void Subscribe(EnemyHealth health)
    {
        if(curEnemyBarTimer <= 0)
        {
            enemyHPBar.ShowBar(true);
        }

        if(subscribedEnemy != health)
        {
            if(subscribedEnemy != null)
            {
                subscribedEnemy.OnHealthChanged -= UpdateEnemyHPData;
            }
            subscribedEnemy = health;
            subscribedEnemy.OnHealthChanged += UpdateEnemyHPData;

            enemyHPBar.EnemyName = health.gameObject.name;
        }
    }

    private void UpdateEnemyHPData(int cur, int max)
    {
        enemyHPBar.HP = cur;
        enemyHPBar.MaxHP = max;
        curEnemyBarTimer = enemyBarTimer;
    }

    private void Update()
    {
        if(curEnemyBarTimer > 0)
        {
            curEnemyBarTimer -= Time.deltaTime;
            if(curEnemyBarTimer <= 0)
            {
                enemyHPBar.ShowBar(false);
            }
        }
    }
}
