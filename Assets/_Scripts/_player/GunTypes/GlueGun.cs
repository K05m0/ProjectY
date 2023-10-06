using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueGun : MonoBehaviour, IGunTypes
{
    so_GunParameters parameters;
    private Transform firePlace;
    public void ChangeGun(Transform gunPlace, so_GunParameters parameters)
    {
        Debug.Log("3");
        int count = gunPlace.childCount;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Destroy(gunPlace.GetChild(i).gameObject);
            }
        }
        this.parameters = parameters;
        var gun = Instantiate(parameters.gunPrefab, gunPlace);
        gun.AddComponent<GlueGun>();

        firePlace = gun.transform.GetChild(0);
    }

    public void Shoot(KeyCode shootKey)
    {
        if (Input.GetKeyDown(shootKey))
        {
            var bullet = Instantiate(parameters.bulletPrefab, firePlace.position, Quaternion.identity);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePlace.forward * parameters.shootPower, ForceMode.Impulse);
        }
    }

    public void ShootAnim()
    {
        throw new System.NotImplementedException();
    }

}
