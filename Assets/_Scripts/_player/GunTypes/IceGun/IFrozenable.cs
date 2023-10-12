using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFrozenable
{
    void Freezing(float damage , float freezingPercent);
    void Unfreezing(float multiplayer);
    void StartDeleyToUnfreez(float duration);
    bool isFreezing { get; set; }
}
