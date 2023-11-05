using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    Vector3 target;
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.agent.Stop();
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("goodbay in " + this.GetType().Name);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        target = enemy.fielOfView.playerTransform.position;
        if (Vector3.Distance(enemy.transform.position, target) > enemy.stats.attackRange)
        {
            enemy.SwitchState(enemy.chaseState);
        } 
}
}
