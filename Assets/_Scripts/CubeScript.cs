using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IBurnable, IDamageable
{
    [SerializeField] private GameObject fireParticle;

    [SerializeField] private float _health;
    [SerializeField] private bool _isBurning;

    private Coroutine burnDamageCoroutine;
    private Coroutine burnCoroutine;
    public float Health { get => _health; set => _health = value; }
    public bool isBurning { get => _isBurning; set => _isBurning = value; }

    // burning Test Cube
    public void StartDelayToDisableBurn(float Duration)
    {
        Debug.Log(Duration);
        if(burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
        StartCoroutine(DelayDisableBurn(Duration));
    }
    public void StartBurning(float DamagePerSecond)
    {
        isBurning = true;
        if(burnDamageCoroutine != null)
        {
            StopCoroutine(burnDamageCoroutine);
        }
        burnDamageCoroutine = StartCoroutine(BurnDamage(DamagePerSecond));
        fireParticle.SetActive(true);
    }
    public void StopBurning()
    {
        Debug.Log("stop");
        isBurning = false;
        if (burnDamageCoroutine != null)
        {
            StopCoroutine(burnDamageCoroutine);
        }
        fireParticle.SetActive(false);
    }
    private IEnumerator DelayDisableBurn(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        StopBurning();
    }
    private IEnumerator BurnDamage(float damagePerSecond)
    {
        float minTimeToDealDmg = 1f / damagePerSecond;
        WaitForSeconds wait = new WaitForSeconds(minTimeToDealDmg);
        float damagePerTick = Mathf.FloorToInt(minTimeToDealDmg) + 1;

        TakeDamage(damagePerTick);
         while(isBurning)
        {
            yield return wait;
            TakeDamage(damagePerTick);
        }
    }
    public void TakeDamage(float Damage)
    {
        Health -= Damage;
         
        if(Health <= 0)
        {
            Health = 0;
            Destroy(gameObject); // create deathgamevent use criptableobject event patern;
            StopBurning();
        }

    }





    //freeze test Cube
}
