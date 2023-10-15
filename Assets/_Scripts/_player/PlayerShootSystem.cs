using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private List<so_GunParameters> allGuns;
    private so_GunParameters parametersToSend;

    [SerializeField] private Transform gunPlace;
    private IGunTypes gunTypes;
    public void GunSetUp()
    {
        foreach (so_GunParameters gun in allGuns)
        {
            gun.currAmmo = gun.maxAmmo;
        }
    }
    public void Shoot(KeyCode shootKey)
    {
        if (gunTypes == null)
        { return; }

        gunTypes.Shoot(shootKey);
    }
    public void ChangeGun() // zmienić by nie podmieniało modelu a jedynie typ (ostatecznie to będzie 1 gun z różnymi trybami)
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
                parametersToSend = allGuns[index];
                break;

            case 1:
                gunTypes = new IceGun();
                parametersToSend = allGuns[index];
                break;

            case 2:
                gunTypes = new WaterGun();
                parametersToSend = allGuns[index];
                break;

            case 3:
                break;
        }
    }
}
