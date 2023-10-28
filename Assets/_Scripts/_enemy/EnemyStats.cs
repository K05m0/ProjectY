using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("health")]
    public float maxHealth;
    public float currHealth;

    [Header("patrolling")]
    public bool waypointPatroll;
    public float patrollSpeed;
    [Range(0, 360)] public float viewAngleWhilePatroll = 45f;

    public Transform waypointsHolder;
    public List<Vector3> waypoints;

    [Range(0, 20f)] public float rangeWhileFreePatroll;

    private Vector3 pos;

    [Header("chase")]
    public float chaseSpeed;
    public float chaseRange;
    [Range(0, 360)] public float viewAngleWhileChase = 360;


    [Header("field of view")]
    [HideInInspector] public float viewAngle;
    [HideInInspector] public float seeRange;
    public bool drawFieldOfView;
    [Range(0, 20f)] public float seeRangeWhileChase = 10f;
    [Range(0, 20f)] public float seeRangeWhilePatroll = 7f;

    [Header("attack")]
    [Range(0, 5f)] public float attackRange;

    public float Health { get => currHealth; set => currHealth = value; }

    private void Awake()
    {
        currHealth = maxHealth;
        pos = transform.position;
    }

    public void SetUpWaypoints()
    {
        for (int i = 0; i < waypointsHolder.childCount; i++)
        {
            waypoints.Add(waypointsHolder.GetChild(i).transform.position);
            Destroy(waypointsHolder.GetChild(i).gameObject);
        }
    }

    public void FieldOfViewWhileChase()
    {
        seeRange = seeRangeWhileChase;
        viewAngle = viewAngleWhileChase;
    }

    public void FieldOfViewWhilePatroll()
    {
        seeRange = seeRangeWhilePatroll;
        viewAngle = viewAngleWhilePatroll;
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            Destroy(gameObject);
            //TO DO: Death implement
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints.Count > 0)
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.DrawWireSphere(waypoints[i], 2.5f);
            }
        }

        if (!waypointPatroll && pos != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pos, rangeWhileFreePatroll);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }
}
