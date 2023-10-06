using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGun : MonoBehaviour, IGunTypes
{
    so_GunParameters parameters;

    private Transform firePlace;
    private GameObject fireRangeCollider;

    private const string tagFirePlace = "FirePlace";
    private const string tagRangeCollider = "RangeCollider";

    public void ChangeGun(Transform gunPlace, so_GunParameters parameters)
    {
        Debug.Log("2");
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
        gun.AddComponent<IceGun>();

        if (firePlace == null || fireRangeCollider == null)
        {
            for (int i = 0; i < gun.transform.childCount; i++)
            {
                if (gun.transform.GetChild(i).CompareTag(tagFirePlace))
                    firePlace = gun.transform.GetChild(i);

                if (gun.transform.GetChild(i).CompareTag(tagRangeCollider))
                    fireRangeCollider = gun.transform.GetChild(i).gameObject;
            }
        }
    }

    public void Shoot(KeyCode shootKey)
    {
        if (Input.GetKey(shootKey))
        {
        }
    }

    public void ShootAnim()
    {
        throw new System.NotImplementedException();
    }
}
