using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour , IDamageable
{
    public float maxHealth;
    public float currHealth;

    public float patrollSpeed;
    public float chaseSpeed;

    public float seeRange;
    public float seeOtherEnemysRange;
    public float viewAngle;

    public float Health { get => currHealth; set => currHealth = value; }

    private void Awake()
    {
        currHealth = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0 ) 
        {
            Destroy(gameObject);
            //Death implement
        }
    }
}
