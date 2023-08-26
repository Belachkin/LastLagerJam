using System;
using System.Collections;
using System.Collections.Generic;
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
    private Coroutine _attackCoroutine;



    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _attackCoroutine == null)
        {
            SetRandomHit();           
        }             
    }

    private void SetRandomHit()
    {
               
        _attackCoroutine = StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        
        _animator.SetInteger("arms", WeaponAnimations[_attackIndex++ % WeaponAnimations.Length]);

        yield return new WaitForSeconds(AnimationTime);

        if(UseCount > 0)
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

        _attackCoroutine = null;
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
}


