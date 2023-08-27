using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    

    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject[] _weapons;
    public int IdleAnimation = 13;
    public int[] WeaponAnimations = { 14, 15};

    public float Damage = 10;
    public int UseCount;
    public float AnimationTime =.5f;
    private int _attackIndex;
    public bool isArm = true;
    private int[] _armWeaponAnimations = { 14, 15 };

    private GameObject _weapon;
    

    [Header("Хп ебучее и остальная хуйня")]
    [SerializeField] private Health _health;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayers;

    [Header("Черкаши т.е частицы говна")]
    [SerializeField] private ParticleSystem _hitParticle;
    //[SerializeField] private ParticleSystem _takeDamageParticle;

    private bool _isAttacking = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _isAttacking == false)
        {
            _isAttacking = true;
            SetRandomHit();           
        }             
    }

    private void SetRandomHit()
    {
               
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        
        _animator.SetInteger("arms", WeaponAnimations[_attackIndex++ % WeaponAnimations.Length]);

        yield return new WaitForSeconds(AnimationTime);

        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log(Damage);

            if (hitEnemies != null)
            {
                enemy.gameObject.GetComponent<Enemy>().TakeDamage(Damage);

                Instantiate(_hitParticle, _attackPoint);
            }
        }

        if (UseCount > 0)
        {
            UseCount--;
        }

        if (!isArm && UseCount == 0)
        {
            isArm = true;
            Damage = 10;

            IdleAnimation = 13;
            WeaponAnimations = _armWeaponAnimations;
            AnimationTime = .5f;
            _weapon.SetActive(false);
        }


        _animator.SetInteger("arms", IdleAnimation);

        _isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    internal void ChangeWeapon(string name)
    {
        foreach (var item in _weapons)
        {
            if(item.name == name)
            {
                _weapon = item;
                _weapon.SetActive(true);
                break;
            }
        }
        _animator.SetInteger("arms", IdleAnimation);
    }

    public void TakeDamage(float damage)
    {
        _health.Value -= damage;

        //Instantiate(_takeDamageParticle, _attackPoint);

        Debug.Log(_health.Value);
        if (_health.Value <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("УМЕР НАХУЙ!");
    }
}


