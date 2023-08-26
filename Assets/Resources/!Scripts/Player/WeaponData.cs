using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private int _useCount;
    [SerializeField] private int[] _attackAnimationNumber;
    [SerializeField] private int _idleAnimation;
    [SerializeField] private float _animationTime;
    
    public string Name => _name;
    public float Damage => _damage;
    public int UseCount => _useCount;
    public int[] AttackAnimationNumber => _attackAnimationNumber;
    public int idleAnimation => _idleAnimation;

    public float AnimationTime => _animationTime;

}
