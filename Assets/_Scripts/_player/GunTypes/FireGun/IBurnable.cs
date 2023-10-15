using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurnable
{
    void StartDelayToDisableBurn(float Duration);
    bool isBurning { get; set; }
    void StartBurning(float DamagePerSecond);
    void StopBurning();
}
