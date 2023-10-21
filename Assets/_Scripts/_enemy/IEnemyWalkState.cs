using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyWalkState
{
    Vector3 target { get; set; }

    void FindTargetPoint(List<Transform> waypointsList = null);
    void MoveToTarget(NavMeshAgent agent);
    float DistanceCheck(Transform enemyTransform);
}
