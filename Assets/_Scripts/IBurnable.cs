using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurnable
{
    bool isBurning { get; set; }
    void StartBurning(int DamagePerSecond);
    void StopBurning();
}
