using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TestEnemy : MonoBehaviour, IBurnable, IFrozenable, IWetable, IDamageable
{
    [Header("Fire")]
    [SerializeField] private GameObject fireParticle;
    [SerializeField] private bool _isBurning;
    [SerializeField] private float unfreezeMultiplayer;

    private Coroutine burnDamageCoroutine;
    private Coroutine burnCoroutine;

    public bool isBurning { get => _isBurning; set => _isBurning = value; }

    [Header("Ice")]
    [SerializeField] private GameObject iceParticle;

    [SerializeField] private float unfreezeSpeed;
    [SerializeField] private float unfreezeTimeBetweenTick;
    [SerializeField] private bool _isFreezing;

    public float freezeValue;

    private Coroutine unfreezingCoroutine;
    private bool isStoped;

    public bool isFreezing { get => _isFreezing; set => _isFreezing = value; }

    [Header("Water")]

    [SerializeField] private bool _isWet;
    [SerializeField] private GameObject waterParticle;
    [SerializeField] private float wetFreezingMultiplayer;

    private Coroutine dryCoroutine;
    public bool isWet { get => _isWet; set => _isWet = value; }

    [Header("Health")]
    [SerializeField] private float _health;
    public float Health { get => _health; set => _health = value; }

    //tempolary to debuging
    private float baseEmisionRate = 50f;



    // fire Test Base Done
    #region fire
    public void StartDelayToDisableBurn(float Duration)
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }
        burnCoroutine =  StartCoroutine(DelayDisableBurn(Duration));
    }
    public void StartBurning(float DamagePerSecond)
    {
        if (!isFreezing && !isWet)
        {
            isBurning = true;
            if (burnDamageCoroutine != null)
            {
                StopCoroutine(burnDamageCoroutine);
            }
            if(burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
            }
            burnDamageCoroutine = StartCoroutine(BurnDamage(DamagePerSecond));
            fireParticle.SetActive(true);
        }
        else if (isFreezing && !isWet)
        {
            if (!isStoped && unfreezingCoroutine != null)
            {
                StopCoroutine(unfreezingCoroutine);
                unfreezingCoroutine = StartCoroutine(UnfreezingWithDeley(0, unfreezeMultiplayer));
                isStoped = true;
            }
        }
        else if(!isFreezing && isWet)
        {
            if(dryCoroutine != null)
            {
                StopCoroutine(dryCoroutine);
            }
            StopBeingWet();
        }
    }
    public void StopBurning()
    {
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
        while (isBurning)
        {
            yield return wait;
            TakeDamage(damagePerTick);
        }
    }
    #endregion
    // ice test Base Done;
    #region ice
    public void Freezing(float damage, float freezingPercent)
    {

        if (!isBurning)
        {
            isStoped = false;
            isFreezing = true;
            if(isWet)
            {
                freezeValue += freezingPercent * wetFreezingMultiplayer * Time.deltaTime;
            }
            else
            {
                freezeValue += freezingPercent * Time.deltaTime;
            }
            freezeValue = Mathf.Clamp01(freezeValue);
            SetUpParticleSpeed();
            if (unfreezingCoroutine != null)
            {
                StopCoroutine(unfreezingCoroutine);
            }
            iceParticle.SetActive(true);
            TakeDamage(damage);
        }
        else
        {
            StopBurning();
        }
    }
    private void SetUpParticleSpeed()
    {
        if (iceParticle.activeSelf == true)
        {
            var iceParticleSystem = iceParticle.GetComponent<ParticleSystem>();
            var emission = iceParticleSystem.emission;
            emission.rateOverTime = baseEmisionRate * freezeValue;
        }
        if (freezeValue <= 0)
            iceParticle.SetActive(false);
    } // tempolary to debuging
    public void Unfreezing(float multiplayer = 1)
    {
        freezeValue -= Time.deltaTime * unfreezeSpeed * multiplayer;
        freezeValue = Mathf.Clamp01(freezeValue);
        SetUpParticleSpeed();
    }
    public void StartDeleyToUnfreez(float duration)
    {
        if (unfreezingCoroutine != null)
        {
            StopCoroutine(unfreezingCoroutine);
        }
        unfreezingCoroutine = StartCoroutine(UnfreezingWithDeley(duration));
    }
    private IEnumerator UnfreezingWithDeley(float Duration, float multiplayer = 1)
    {
        yield return new WaitForSeconds(Duration);
        while (freezeValue > 0)
        {
            yield return new WaitForSeconds(unfreezeTimeBetweenTick);
            Unfreezing(multiplayer);
        }
        isFreezing = false;
    }
    #endregion
    // water Base Done bv 
    #region Water
    public void StartBeingWet()
    {
        if(!isBurning &&  !isFreezing)
        {
            isWet = true;
            waterParticle.SetActive(true);
            if (dryCoroutine != null)
            {
                StopCoroutine(dryCoroutine);
            }
        }
        else if(isBurning && ! isFreezing)
        {
            StopBurning();
        }
        else if(!isBurning && isFreezing)
        {
            return;
        }
    }

    public void StopBeingWet()
    {
        isWet = false;
        waterParticle.SetActive(false);
        if (dryCoroutine != null)
        {
            StopCoroutine(dryCoroutine);
        }
    }

    public void StartDeleyToDry(float duration)
    {

        if (dryCoroutine != null)
        {
            StopCoroutine(dryCoroutine);
        }
        StartCoroutine(DeleyToDry(duration));
    }

    private IEnumerator DeleyToDry(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopBeingWet();
    }
    #endregion

    public void TakeDamage(float Damage)
    {
        Health -= Damage;

        if (Health <= 0)
        {
            Health = 0;
            Destroy(gameObject); // create deathgamevent use criptableobject event patern;
            StopBurning();
        }
    }
}
