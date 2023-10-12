using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FireGun: IGunTypes
{
    private Transform firePlace;
    private GameObject fireRangeCollider;
    private GameObject particle;

    private const string tagFirePlace = "FirePlace";
    private const string tagRangeCollider = "RangeCollider";
    private const string tagFireParticle = "ParticleSystem";

    public void ChangeGun(Transform gunPlace, so_GunParameters parameters)
    {
        foreach (Transform child in gunPlace)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject instanceGun = GameObject.Instantiate(parameters.gunPrefab, gunPlace);

        if (firePlace == null || fireRangeCollider == null || particle == null)
        {
            for (int i = 0; i < instanceGun.transform.childCount; i++)
            {
                if (instanceGun.transform.GetChild(i).CompareTag(tagFirePlace))
                {
                    firePlace = instanceGun.transform.GetChild(i);
                    continue;
                }
                if (instanceGun.transform.GetChild(i).CompareTag(tagRangeCollider))
                {
                    fireRangeCollider = instanceGun.transform.GetChild(i).gameObject;
                    continue;
                }
                if (instanceGun.transform.GetChild(i).CompareTag(tagFireParticle))
                {
                    particle = instanceGun.transform.GetChild(i).gameObject;
                    continue;
                }
            }
        }

    }
    public void Shoot(KeyCode shootKey)
    {
        if (Input.GetKey(shootKey))
        {
            fireRangeCollider.SetActive(true);
            particle.SetActive(true);
        }
        else if (Input.GetKeyUp(shootKey))
        {
            fireRangeCollider.SetActive(false);
            particle.SetActive(false);
        }
    }
    public void ShootAnim()
    {
        throw new System.NotImplementedException();
    }
}
