using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class State
{
    protected Enemy _enemy;
    protected StateMachine _stateMachine;

    public State(Enemy enemy, StateMachine stateMachine)
    {
        this._enemy = enemy;
        this._stateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
