using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollState : EnemyState
{
    private Transform startPos;
    private Vector3 target;
    public override void EnterState(EnemyStateManager enemy)
    {
        if (startPos == null)
        {
            startPos = enemy.transform;
        }
        FindNewTarget(enemy.stats, enemy.agent);
    }

    public override void ExitState(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (Vector3.Distance(enemy.transform.position, target) < enemy.agent.stoppingDistance)
        {
            FindNewTarget(enemy.stats, enemy.agent);
        }
    }

    private void FindNewTarget(EnemyStats stats, NavMeshAgent agent)
    {
        if (stats.waypointPatroll)
        {
           
        }
        else
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * stats.rangeWhileFreePatroll;
            randomDirection += startPos.position;
            NavMeshHit hit; 
            NavMesh.SamplePosition(randomDirection, out hit, stats.rangeWhileFreePatroll, NavMesh.AllAreas);
            target = hit.position;
        }
        Debug.Log("find");
        agent.SetDestination(target);
    }
}
