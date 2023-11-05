using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private Vector3 target;
    public override void EnterState(EnemyStateManager enemy)
    {
        target = enemy.fielOfView.playerTransform.position;
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("goodbay in " + this.GetType().Name);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (!enemy.fielOfView.isPlayerInFieldOfView)
        {
            enemy.SwitchState(enemy.patrollState);
            return;
        }
        target = enemy.fielOfView.playerTransform.position;
        enemy.agent.SetDestination(target);
        if (Vector3.Distance(enemy.transform.position, target) <= enemy.stats.attackRange)
        {
            enemy.SwitchState(enemy.attackState);
            return;
        }

    }
}
