using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStayState : IEnemyWalkState
{
    public Vector3 target { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float DistanceCheck(Transform enemyTransform)
    {
        throw new System.NotImplementedException();
    }

    public void FindTargetPoint(List<Transform> waypointsList = null)
    {
        throw new System.NotImplementedException();
    }

    public void MoveToTarget(NavMeshAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
