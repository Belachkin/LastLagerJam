using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // �� ��� ���� ���� ��� ������

    [SerializeField] private Rigidbody[] _rigs;
    
    private StateMachine _enemySM;
    public IdleEnemyState _idleEnemyState;
    public MovingEnemyState _movingEnemyState;
    public FightEnemyState _fightEnemyState;

    public NavMeshAgent _navMeshAgent;
    public Animator _animator;
    [SerializeField] private float _speed;

    [HideInInspector]
    public PlayerMovement _player;
    public float _distanceToPlayerWithoutRage;
    public float _distanceToPlayerToAttack;

    [Header("����� � ����� � �����")]
    public int _idleArms = 5;
    public int _idleLegs = 5;
    public int[] _attackArms = {14, 15 };
    public float _attackTimer = 1f;
    public GameObject _prop;

    [Header("�� ������ � ��������� �����")]
    [SerializeField] private float _baseDamage = 5;
    [SerializeField] private Health _health;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _playerLayer;

    [Header("������� �.� ������� �����")]
    [SerializeField] private ParticleSystem _hitParticle;

    [Header("������������")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _knockbackPower = 16f;
    [SerializeField] private float _resetDelay = 0.15f;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>();

        _enemySM = new StateMachine();

        _idleEnemyState = new IdleEnemyState(this, _enemySM);
        _movingEnemyState = new MovingEnemyState(this, _enemySM);
        _fightEnemyState = new FightEnemyState(this, _enemySM);

        _fightEnemyState.AttackPoint = _attackPoint;
        _fightEnemyState.AttackRange = _attackRange;
        _fightEnemyState.PlayerLayer = _playerLayer;
        _fightEnemyState.Damage = _baseDamage;
        

        _enemySM.Initialize(_idleEnemyState);

        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        _enemySM.CurrentState.LogicUpdate();

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.V)) 
        {
            _animator.enabled = false;
            GetComponent<Collider>().enabled = false;
            _navMeshAgent.enabled = false;
            this.enabled = false;
        }
#endif
    }

    private void FixedUpdate()
    {
        _enemySM.CurrentState.PhysicsUpdate();
    }

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;
        Debug.Log(_health.Value);

        Knockback();

        if( _health.Value <= 0 )
        {
            Die();
        }
    }

    public void Die()
    {
        _animator.enabled = false;
        GetComponent<Collider>().enabled = false;
        _navMeshAgent.enabled = false;
        this.enabled = false;

        if(Random.Range(0, 3) == 0)
        {
            AudioManager.instance.Play("dieCigan");
        }


        foreach(Rigidbody rig in _rigs)
        {
            rig.isKinematic = false;
        }
    }

    public void Knockback()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce((transform.position - _player.transform.position).normalized * _knockbackPower, ForceMode.Impulse );
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(_resetDelay);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;

    }

    public void InstantParticle()
    {
        Instantiate(_hitParticle, _attackPoint);
    }

}
