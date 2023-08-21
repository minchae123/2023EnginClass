using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    private PlayerInput playerInput;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.OnFire += Shoot;
    }

    private void Shoot()
    {
        Instantiate(bullet, firePos.position, Quaternion.identity);

        Destroy(gameObject, 2f);
    }
}
