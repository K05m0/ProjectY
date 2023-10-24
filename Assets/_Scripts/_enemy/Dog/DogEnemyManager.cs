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
    private NavMeshAgent agent;

    private void Awake()
    {
        fieldOfView = GetComponent<DogEnemyFieldOfView>();
        movement = GetComponent<DogEnemyMovement>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
    }

    private void Start()
    {
        StartCoroutine(fieldOfView.FindPlayerWithDeley(stats));
        movement.SetUpWaypoints();
    }

    private void Update()
    {
        if (!fieldOfView.isPlayerInFieldOfView)
        {
            stats.FieldOfViewWhileChase();
        }
        else
        {
            stats.FieldOfViewWhilePatroll();
        }

        movement.WalkStateController(fieldOfView.isPlayerInFieldOfView, fieldOfView.playerTransform, agent);
        movement.MovementScript(agent);
    }

    private void FixedUpdate()
    {
        fieldOfView.DrawEnemyFieldOfView(stats);
    }
}
