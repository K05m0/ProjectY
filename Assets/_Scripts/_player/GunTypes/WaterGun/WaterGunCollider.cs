using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunCollider : MonoBehaviour
{
    [SerializeField] private List<GameObject> wetObject = new List<GameObject>();
    [SerializeField] private so_GunParameters parameters;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IWetable>(out IWetable wet))
        {
            wetObject.Add(other.gameObject);
            wet.StartBeingWet();
        }
        else
        {
            var parent = other.transform.parent;
            if(parent.TryGetComponent<IWetable>(out IWetable wetParent))
            {
                wetObject.Add(parent.gameObject);
                wetParent.StartBeingWet();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IWetable>(out IWetable wet))
        {
            wetObject.Remove(other.gameObject);
            wet.StartDeleyToDry(parameters.gunEffectDuration);
        }
        else
        {
            var parent = other.transform.parent;
            if(parent.TryGetComponent<IWetable>(out IWetable wetParent))
            {
                wetObject.Remove(parent.gameObject);
                wet.StartDeleyToDry(parameters.gunEffectDuration);
            }
        }
    }

    private void OnDisable()
    {
        foreach (GameObject obj in wetObject)
        {
            if (obj != null && obj.TryGetComponent<IWetable>(out IWetable wet))
                wet.StartDeleyToDry(parameters.gunEffectDuration);
        }
        wetObject.Clear();
    }
}
