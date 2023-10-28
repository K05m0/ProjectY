using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyFieldOfView fieldOfView;
    [SerializeField] private EnemyStats stats;

    private void Awake()
    {
        fieldOfView = GetComponent<EnemyFieldOfView>();
    }
    private void Start()
    {
        stats.SetUpWaypoints();
        StartCoroutine(fieldOfView.FindPlayerWithDeley(stats));
    }

    private void Update()
    {
        if (fieldOfView.isPlayerInFieldOfView)
        { stats.FieldOfViewWhileChase(); }
        else
        { stats.FieldOfViewWhilePatroll(); }
    }

    private void FixedUpdate()
    {
        if (stats.drawFieldOfView)
        {
            Debug.Log("mleko");
            fieldOfView.DrawEnemyFieldOfView(stats); 
        }
    }
}
