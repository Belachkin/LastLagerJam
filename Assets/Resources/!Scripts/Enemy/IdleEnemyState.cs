using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class IdleEnemyState : State
{
    public IdleEnemyState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        if(_enemy._prop != null)
            _enemy._prop.SetActive(true);
        _enemy._animator.SetInteger("legs", _enemy._idleLegs);
        _enemy._animator.SetInteger("arms", _enemy._idleArms);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float distanceToPlayer = (_enemy.transform.position - _enemy._player.transform.position).magnitude;

        Debug.Log("fgghs");

        if (distanceToPlayer < _enemy._distanceToPlayerWithoutRage)
            _stateMachine.ChangeState(_enemy._movingEnemyState);
    }

    public override void Exit()
    {
        if(_enemy._prop != null)
            _enemy._prop.SetActive(false);
    }
}