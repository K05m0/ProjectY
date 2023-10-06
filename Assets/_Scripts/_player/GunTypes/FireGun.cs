using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour, IGunTypes
{
    private so_GunParameters parameters;

    private Transform firePlace;
    private GameObject fireRangeCollider;
    private GameObject particle;



    private const string tagFirePlace = "FirePlace";
    private const string tagRangeCollider = "RangeCollider";
    private const string tagFireParticle = "ParticleSystem";
    public void ChangeGun(Transform gunPlace, so_GunParameters parameters)
    {
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

        if(firePlace == null || fireRangeCollider == null || particle == null)
        {
            for (int i = 0; i < gun.transform.childCount; i++)
            {
                if(gun.transform.GetChild(i).CompareTag(tagFirePlace))
                {
                    firePlace = gun.transform.GetChild(i);
                    continue;
                }
                if (gun.transform.GetChild(i).CompareTag(tagRangeCollider))
                {
                    fireRangeCollider = gun.transform.GetChild(i).gameObject;
                    continue;
                }
                if (gun.transform.GetChild(i).CompareTag(tagFireParticle))
                {
                    particle = gun.transform.GetChild(i).gameObject;
                    continue;
                }
            }
        }
    }
    public void Shoot(KeyCode shootKey)
    {
        if(Input.GetKey(shootKey))
        {
            fireRangeCollider.SetActive(true);
            particle.SetActive(true);
        }
        else if(Input.GetKeyUp(shootKey))
        {
            fireRangeCollider.SetActive(false);
            particle.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    public void ShootAnim()
    {
        throw new System.NotImplementedException();
    }
}
