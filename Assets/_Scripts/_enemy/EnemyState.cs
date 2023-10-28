using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void ExitState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);

}
