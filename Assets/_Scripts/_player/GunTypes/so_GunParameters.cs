using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class so_GunParameters : ScriptableObject
{
    [Header("Gun Prefabs")]
    public GameObject bulletPrefab;
    public GameObject gunPrefab;

    [Header("Gun Dmg Parameters")]
    public float gunBaseDmg;
    public float gunEffectDmg;
    public float gunEffectDuration;

    [Header("Gun Ammo Parameters")]
    public float maxAmmo;
    public float currAmmo;
    public float useAmmoPerSec;
    public float regenAmmoPerSec;
    public float delayToRegen;
    public bool isRenegerate;

    [Header("Gun Shoot Parameters")]
    public float shootPower;
}
