using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IEnemyWalkState
{
    private NavMeshAgent agent;
    private Vector3 target;

    public ChaseState(NavMeshAgent agent, Transform playerTransform)
    {
        this.agent = agent;
        this.target = playerTransform.position;
        agent.SetDestination(target);
    }

    public void FindNewTarget()
    {
    }

    public void MoveToTarget()
    {
    }
}
