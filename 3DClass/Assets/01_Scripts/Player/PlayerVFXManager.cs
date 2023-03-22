using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] blades;

    [SerializeField] private VisualEffect footStep;

    private AttackState atkState;

    private void Awake()
    {
        atkState = transform.Find("States").GetComponent<AttackState>();
        atkState.OnAttackStart += PlayBlade;
        atkState.OnAttackEnd += StopBlade;
    }

    public void UpdateFootStep(bool state)
    {
        if (state)
        {
            footStep.Play();
        }
        else
        {
            footStep.Stop();
        }
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
