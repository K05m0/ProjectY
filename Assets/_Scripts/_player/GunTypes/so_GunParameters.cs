using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class so_GunParameters : ScriptableObject
{
    public GameObject bulletPrefab;
    public GameObject gunPrefab;

    public float shootPower;
    public float maxAmmo;
    public float currAmmo;

}
