using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyState : State 
{
    private float _timer = 0f;
    private int _attackIndex = 0;
    
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask PlayerLayer;
    public float Damage;
    
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

        Vector3 diff = _enemy.transform.position - _enemy._player.transform.position;
        float distanceToPlayer = (diff).magnitude;

        if (distanceToPlayer > _enemy._distanceToPlayerToAttack)
            _stateMachine.ChangeState(_enemy._movingEnemyState);

        float angle = Vector3.SignedAngle(Vector3.forward, -diff, Vector3.up);
        _enemy.transform.eulerAngles = new Vector3(0, angle, 0);

        if (_timer > _enemy._attackTimer)
        {
            _enemy._animator.SetInteger("arms", _enemy._attackArms[_attackIndex++ % _enemy._attackArms.Length]);

            Attack();

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

    public void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(AttackPoint.position, AttackRange, PlayerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("Enemy take dmg:" + Damage);

            if (hitPlayer != null)
            {
                player.gameObject.GetComponent<PlayerAttack>().TakeDamage(Damage);
                _enemy.InstantParticle();
                AudioManager.instance.Play("hit1");
            }
        }
    }
}
