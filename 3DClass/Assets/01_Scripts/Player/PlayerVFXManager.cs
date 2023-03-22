using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] blades;

    private AttackState atkState;

    private void Awake()
    {
        atkState = transform.Find("States").GetComponent<AttackState>();
        atkState.OnAttackStart += PlayBlade;
        atkState.OnAttackEnd += StopBlade;
    }

    private void PlayBlade(int combo)
    {
        blades[combo - 1].Play();
    }

    private void StopBlade()
    {
        foreach(ParticleSystem p in blades)
        {
            p.Simulate(0);
            p.Stop();
        }
    }
}
