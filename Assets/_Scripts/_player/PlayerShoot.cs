using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private List<so_GunParameters> allGuns;
    private so_GunParameters parametersToSend;

    [SerializeField] private Transform gunPlace;
    private IGunTypes gunTypes;

    [SerializeField] private GunType type;


    private void Awake()
    {
    }

    public void Shoot(KeyCode shootKey)
    {
        if (gunTypes == null)
        { return; }

        gunTypes.Shoot(shootKey);
    }
    public void ChangeGun()
    {
        if (gunTypes == null)
        { return; }

        gunTypes.ChangeGun(gunPlace, parametersToSend);
    }
    public void CheckGunType(int index)
    {
        switch (index)
        {
            case 0:
                gunTypes = new FireGun();
                type = GunType.fire;
                parametersToSend = allGuns[index];
                break;

            case 1:
                gunTypes = new IceGun();
                type = GunType.ice;
                parametersToSend = allGuns[index];
                break;

            case 2:
                gunTypes = new GlueGun();
                type = GunType.glue;
                parametersToSend = allGuns[index];
                break;
        }
    }

    private enum GunType { fire, ice, glue }
}
