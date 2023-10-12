using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyMovement : MonoBehaviour
{
    public Transform waypointManager;
    [SerializeField, Range(0f, 1f)] private float minMoveSpeed;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;
    private float baseMoveSpeed;
    [SerializeField] private float moveSpeed;

    private TestEnemy test;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        test = GetComponent<TestEnemy>();

        foreach (Transform waypoint in waypointManager)
        {
            waypoints.Add(waypoint);
        }
        MoveToNextWaypoint();

        baseMoveSpeed = agent.speed;
    }

    private void UpdateMoveSpeed()
    {
        var slowValue = (1 - test.freezeValue) + minMoveSpeed;

        if (test.freezeValue < 1)
            moveSpeed = baseMoveSpeed * slowValue;
        else
            moveSpeed = 0;

        agent.speed = moveSpeed;
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            Transform nextWaypoint = waypoints[currentWaypointIndex];
            agent.SetDestination(nextWaypoint.position);
            currentWaypointIndex++;
        }
        else
        {
            currentWaypointIndex = 0;
        }
    }

    void Update()
    {
        if (agent.remainingDistance < 0.1f && !agent.pathPending)
        {
            // If the agent has reached the current waypoint, move to the next one.
            MoveToNextWaypoint();
        }
        UpdateMoveSpeed();
    }
}
