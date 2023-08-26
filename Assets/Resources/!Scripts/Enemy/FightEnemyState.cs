using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class FightEnemyState : State
{
    private float _timer = 0f;
    private int _attackIndex = 0;

    public FightEnemyState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        _enemy._animator.SetInteger("legs", 5);
        _enemy._animator.SetInteger("arms", 13);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float distanceToPlayer = (_enemy.transform.position - _enemy._player.transform.position).magnitude;

        if (distanceToPlayer > _enemy._distanceToPlayerToAttack)
            _stateMachine.ChangeState(_enemy._movingEnemyState);

        if(_timer > _enemy._attackTimer)
        {
            _enemy._animator.SetInteger("arms", _enemy._attackArms[_attackIndex++ % _enemy._attackArms.Length]);
            _timer = 0f;
        }

        _timer += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {

    }
}
