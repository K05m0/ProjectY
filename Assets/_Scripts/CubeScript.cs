using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IBurnable, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private bool _isBurning;
    public int Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool isBurning { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }


    // burning Test Cube
    public void StartBurning(int DamagePerSecond)
    {
        throw new System.NotImplementedException();
    }

    public void StopBurning()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int Damage)
    {
        throw new System.NotImplementedException();
    }


    //freeze test Cube
}
