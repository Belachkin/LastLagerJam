using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyState : State
{
    public MovingEnemyState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        _enemy._navMeshAgent.isStopped = false;
        _enemy._animator.SetInteger("legs", 2);
        _enemy._animator.SetInteger("arms", 2);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float distanceToPlayer = (_enemy.transform.position - _enemy._player.transform.position).magnitude;
        //Debug.Log("faf");

        _enemy._navMeshAgent.destination = _enemy._player.transform.position;

        if (distanceToPlayer > _enemy._distanceToPlayerWithoutRage)
            _stateMachine.ChangeState(_enemy._idleEnemyState);

        if (distanceToPlayer < _enemy._distanceToPlayerToAttack)
            _stateMachine.ChangeState(_enemy._fightEnemyState);
    }

    public override void Exit()
    {
        _enemy._navMeshAgent.isStopped = true;
    }
}
