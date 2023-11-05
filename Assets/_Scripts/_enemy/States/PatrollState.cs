using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollState : EnemyState
{
    private Vector3 startPos;
    private Vector3 target;
    private int currWaypoint;
    public override void EnterState(EnemyStateManager enemy)
    {
        if (startPos == Vector3.zero)
        {
            startPos = enemy.transform.position;
        }
        FindNewTarget(enemy.stats, enemy.agent, enemy.indicator);
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        Debug.Log("Exit state: " + this.GetType().Name);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.fielOfView.isPlayerInFieldOfView)
        {
            enemy.SwitchState(enemy.chaseState);
            return;
        }

        if (Vector3.Distance(enemy.transform.position, target) <= enemy.agent.stoppingDistance)
        {
            FindNewTarget(enemy.stats, enemy.agent, enemy.indicator);
        }

        if (enemy.agent.velocity.magnitude != 0)
        { return; }

        FindNewTarget(enemy.stats, enemy.agent, enemy.indicator);
        enemy.agent.SetDestination(target);
    }
    private void FindNewTarget(EnemyStats stats, NavMeshAgent agent, Transform moveIndicator)
    {
        if (stats.waypointPatroll)
        {
            if (currWaypoint < stats.waypoints.Count - 1)
            {
                currWaypoint++;
            }
            else
            {
                currWaypoint = 0;
            }
            target = stats.waypoints[currWaypoint];
            moveIndicator.gameObject.SetActive(false);

        }
        else
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * stats.rangeWhileFreePatroll;
            randomDirection += startPos;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, stats.rangeWhileFreePatroll, NavMesh.AllAreas))
                target = hit.position;
            moveIndicator.gameObject.SetActive(true);
            moveIndicator.position = target;
        }
        agent.SetDestination(target);
    }
}
