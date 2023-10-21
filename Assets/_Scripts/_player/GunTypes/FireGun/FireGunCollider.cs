using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireGunCollider : MonoBehaviour
{
    [SerializeField] private List<GameObject> burningObject = new List<GameObject>();
    [SerializeField] private so_GunParameters parameters;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IBurnable>(out IBurnable burn))
        {
            burningObject.Add(other.gameObject);
            burn.StartBurning(parameters.gunEffectDmg);
        }
        else
        {
            var parent = other.transform.parent;
            if(parent.TryGetComponent<IBurnable>(out IBurnable burnParent))
            {
                burningObject.Add(parent.gameObject);
                burnParent.StartBurning(parameters.gunEffectDmg);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IBurnable>(out IBurnable burnable))
        {
            burningObject.Remove(other.gameObject);
            burnable.StartDelayToDisableBurn(parameters.gunEffectDuration);
        }
        else
        {
            var parent = other.transform.parent;
            if (parent.TryGetComponent<IBurnable>(out IBurnable burnParent))
            {
                burningObject.Remove(parent.gameObject);
                burnParent.StartDelayToDisableBurn(parameters.gunEffectDuration);
            }
        }

    }
    private void OnDisable()
    {
        foreach (GameObject obj in burningObject)
        {
            if (obj != null && obj.TryGetComponent<IBurnable>(out IBurnable burnable))
                burnable.StartDelayToDisableBurn(parameters.gunEffectDuration);
        }
        burningObject.Clear();
    }
}
