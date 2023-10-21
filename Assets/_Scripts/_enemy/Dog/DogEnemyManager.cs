using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
public class DogEnemyManager : MonoBehaviour
{
    private DogEnemyFieldOfView fieldOfView;
    private DogEnemyMovement movement;
    private EnemyStats stats;
    private NavMeshAgent navMeshAgent;

    private IEnemyWalkState walkState;

    private void Awake()
    {
        fieldOfView = GetComponent<DogEnemyFieldOfView>();
        movement = GetComponent<DogEnemyMovement>();
        stats = GetComponent<EnemyStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        walkState = movement.WalkStateUpdate(fieldOfView.isPlayerInFieldOfView, stats, navMeshAgent);

        movement.WaypointsSetUp();
        StartCoroutine(fieldOfView.FindPlayerWithDeley(stats));
        StartCoroutine(movement.CheckDistance());
    }

    private void Update()
    {
        walkState = movement.WalkStateUpdate(fieldOfView.isPlayerInFieldOfView, stats, navMeshAgent);

        if (walkState == null)
        { return; }
        movement.Movement(navMeshAgent);

    }

    private void FixedUpdate()
    {
        fieldOfView.DrawEnemyFieldOfView(stats);
    }
}
