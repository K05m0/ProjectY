using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointPatrolling : IEnemyWalkState
{
    private Vector3 target;
    private NavMeshAgent agent;
    private List<Vector3> waypoints;

    private int currWaypoint;
    private bool isReverse = false;
    public CheckpointPatrolling(NavMeshAgent agent, List<Vector3> waypoints)
    {
        this.agent = agent;
        this.waypoints = waypoints;
    }
    public void FindNewTarget()
    {
        Debug.Log(currWaypoint);
        target = waypoints[currWaypoint];
        if (isReverse)
        {
            currWaypoint--;
            if (currWaypoint == 0)
            {
                isReverse = false;
            }
        }
        else
        {
            currWaypoint++;
            if (currWaypoint == waypoints.Count - 1)
            {
                isReverse = true;
            }
        }
    }

    public void MoveToTarget()
    {
        //Debug.Log(agent.remainingDistance + " checkpoint Patrolling");
        agent.SetDestination(target);
    }
}
