using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWetable
{
    void StartBeingWet();
    void StopBeingWet();
    void StartDeleyToDry(float duration);
    bool isWet { get; set; }

}
