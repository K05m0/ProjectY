using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("welcome in " + this.GetType().Name);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("goodbay in " + this.GetType().Name);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Debug.Log("update in " + this.GetType().Name);
        enemy.SwitchState(enemy.patrollState);
    }
}
