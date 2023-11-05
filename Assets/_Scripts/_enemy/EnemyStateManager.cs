using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyStats stats;
    public EnemyFieldOfView fielOfView;
    public NavMeshAgent agent;
    public Transform indicator;

    
    #region State
    private EnemyState currState;
    public PatrollState patrollState = new PatrollState();
    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();
    #endregion

    private void Start()
    {
        currState = patrollState;
        currState.EnterState(this);
    }

    private void Update()
    {
        Debug.Log(currState.ToString());
        currState.UpdateState(this);
    }

    public void SwitchState(EnemyState state)
    {
        currState.ExitState(this);
        currState = state;
        state.EnterState(this);
    }
}
