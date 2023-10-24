using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float currHealth;

    public float patrollSpeed;
    public float chaseSpeed;
    public float chaseRange;




    [HideInInspector] public float seeRange;
    [Range(0, 20f)] public float seeRangeWhileChase = 10f;
    [Range(0, 20f)] public float seeRangeWhilePatroll = 7f;

    [HideInInspector] public float viewAngle;
    [Range(0, 360)] public float viewAngleWhileChase = 360;
    [Range(0, 360)] public float viewAngleWhilePatroll = 45f;
    //public float seeOtherEnemysRange;


    public float Health { get => currHealth; set => currHealth = value; }

    private void Awake()
    {
        currHealth = maxHealth;
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
            //Death implement
        }
    }
}
