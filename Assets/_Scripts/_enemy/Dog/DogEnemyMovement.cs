using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class DogEnemyMovement : MonoBehaviour
{
    [SerializeField] private bool isEnemyStay;
    [SerializeField] private bool isEnemyUseWaypoints;

    [SerializeField] private GameObject waypointsHolder;
    [SerializeField] private List<Transform> waypoints;

    [SerializeField] private float timeBeetwenCheckTick;

    [SerializeField] private float currMoveSpeed;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float distance;

    enum walkStates { chase, chekpoint, freeWalk, stay }
    [SerializeField] private walkStates states;
    private IEnemyWalkState walkState;
    
    public void WaypointsSetUp()
    {
        if (waypointsHolder != null && waypointsHolder.transform.childCount > 0)
        {
            for (int i = 0; i < waypointsHolder.transform.childCount; i++)
            {
                waypoints.Add(waypointsHolder.transform.GetChild(i));
            }
        }
    }
    public IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(timeBeetwenCheckTick);
        if (walkState != null)
        {
            distance = walkState.DistanceCheck(transform);
        }
    }
    public IEnemyWalkState WalkStateUpdate(bool seeEnemy, EnemyStats stats , NavMeshAgent agent)
    {
        IEnemyWalkState walkState;
        if (!isEnemyStay)
        {
            if (seeEnemy)
            {
                walkState = new EnemyChaseState();
                currMoveSpeed = stats.chaseSpeed;
                states = walkStates.chase;
            }
            else
            {
                if (isEnemyUseWaypoints)
                {
                    walkState = new EnemyChekpointState();
                    currMoveSpeed = stats.patrollSpeed;
                    states = walkStates.chekpoint;
                }
                else
                {
                    walkState = new EnemyFreeWalkingState();
                    currMoveSpeed = stats.patrollSpeed;
                    states = walkStates.freeWalk;
                }
            }
        }
        else
        {
            walkState = new EnemyStayState();
            currMoveSpeed = 0;
            states = walkStates.stay;
        }
        agent.speed = currMoveSpeed;
        return walkState;
    }
    public void Movement(NavMeshAgent agent)
    {
        if(distance <= agent.stoppingDistance)
        {
            if (states == walkStates.chekpoint)
            {
                walkState.FindTargetPoint(waypoints);

            }
            else
            {
                walkState.FindTargetPoint();
            }

            if (GroundCheck())
            {
                walkState.MoveToTarget(agent);
            }
            else
            {
                agent.isStopped = true;
            }
        }
        else
        {
            if(agent.isStopped)
            {
                walkState.FindTargetPoint();
                if (GroundCheck())
                {
                    walkState.MoveToTarget(agent);
                }
                else
                {
                    agent.isStopped = true;
                }
            }
        }
    }
    private bool GroundCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out var hit, transform.localScale.y * 0.5f + 0.2f, groundLayerMask))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * (transform.localScale.y * 0.5f + 0.2f), Color.red);
            return false;
        }
    }
}
