using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFreeWalkingState : IEnemyWalkState
{
    public Vector3 _target;
    [Serialize] private float _sphereRange;
    public Vector3 target { get => _target; set => _target = value; }
    public float DistanceCheck(Transform enemyTransform)
    {
        return Vector3.Distance(target, enemyTransform.position);
    }

    public void FindTargetPoint(List<Transform> waypointsList = null)
    {
        Debug.DrawRay(new Vector3(0, 0, 0), Vector3.forward * _sphereRange, Color.red);
    }
    public void MoveToTarget(NavMeshAgent agent)
    {
        agent.SetDestination(target);
    }
}
