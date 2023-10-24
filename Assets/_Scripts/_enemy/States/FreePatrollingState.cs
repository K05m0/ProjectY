using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FreePatrollingState : IEnemyWalkState
{
    private NavMeshAgent agent;
    private Vector3 startPos = new Vector3(0,0,0);
    private float sphereRange;
    private Vector3 target = new Vector3(0, 0, 0);

    public FreePatrollingState(NavMeshAgent agent, Vector3 startPos, float sphereRange)
    {
        if(startPos == new Vector3(0,0,0))
            this.startPos = startPos;
        this.agent = agent;
        this.sphereRange = sphereRange;
    }

    public void FindNewTarget()
    {
        Vector3 randomDiraction = Random.insideUnitSphere * sphereRange;
        randomDiraction += startPos;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDiraction, out hit, sphereRange, NavMesh.AllAreas);
        target = hit.position;
    }

    public void MoveToTarget()
    {
        Debug.Log(agent.remainingDistance + " free patrolling");
        agent.SetDestination(target);
    }

}
