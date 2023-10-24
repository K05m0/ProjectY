using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class DogEnemyMovement : MonoBehaviour
{
    private enum allWalkStates { FreePatrolling, CheckpointPatrolling, Chase }
    private allWalkStates currWalkState;

    [SerializeField] private bool enemyStay;
    [SerializeField] private bool useChekPoint;

    [SerializeField] private float freeWalkSphereRange;

    [SerializeField] private Transform waypointsHolder;
    [SerializeField] private List<Vector3> waypoints;

    private IEnemyWalkState walkState;

    public void SetUpWaypoints()
    {
        if (waypointsHolder.childCount > 0)
        {
            for (int i = 0; i < waypointsHolder.childCount; i++)
            {
                waypoints.Add(waypointsHolder.GetChild(i).position);
            }
        }
    }

    public void WalkStateController(bool seePlayer,Transform playerTransform,  NavMeshAgent agent)
    {
        if (seePlayer)
        {
            currWalkState = allWalkStates.Chase;
            walkState = new ChaseState(agent,playerTransform);
        }
        else
        {
            if (useChekPoint && currWalkState != allWalkStates.CheckpointPatrolling)
            {
                currWalkState = allWalkStates.CheckpointPatrolling;
                walkState = new CheckpointPatrolling(agent, waypoints);
            }
            else if (!useChekPoint && currWalkState != allWalkStates.FreePatrolling)
            {
                currWalkState = allWalkStates.FreePatrolling;
                walkState = new FreePatrollingState(agent, transform.position, freeWalkSphereRange);
            }
        }
    }

    public void MovementScript(NavMeshAgent agent)
    {
        if (walkState == null)
        { return; }

        if (!enemyStay)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                walkState.FindNewTarget();
                walkState.MoveToTarget();
            }
            else
            {
                if (agent.velocity.sqrMagnitude <= 0)
                {
                    walkState.MoveToTarget();
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        if (startPos != new Vector3(0, 0, 0))
            startPos = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPos, freeWalkSphereRange);
    }
}
