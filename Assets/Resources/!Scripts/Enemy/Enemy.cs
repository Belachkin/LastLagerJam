using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    // Ну тут типа враг нас пиздит

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

    [Header("Хуйня в руках и ногах")]
    public int _idleArms = 5;
    public int _idleLegs = 5;
    public int[] _attackArms = {14, 15 };
    public float _attackTimer = 1f;
    public GameObject _prop;

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>();

        _enemySM = new StateMachine();

        _idleEnemyState = new IdleEnemyState(this, _enemySM);
        _movingEnemyState = new MovingEnemyState(this, _enemySM);
        _fightEnemyState = new FightEnemyState(this, _enemySM);

        _enemySM.Initialize(_idleEnemyState);

        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        _enemySM.CurrentState.LogicUpdate();

        if(Input.GetKeyDown(KeyCode.V)) 
        {
            _animator.enabled = false;
            GetComponent<Collider>().enabled = false;
            _navMeshAgent.enabled = false;
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        _enemySM.CurrentState.PhysicsUpdate();
    }

}
