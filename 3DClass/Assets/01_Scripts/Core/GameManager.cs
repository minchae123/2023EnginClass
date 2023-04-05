using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Transform playerTrm;
    public Transform PlayerTrm
    {
        get { if (playerTrm == null)
                playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            return playerTrm;
        }
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;
    }
}
