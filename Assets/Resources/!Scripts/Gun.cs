using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    //[SerializeField] private GameObject _bulletPrefab;
    private void Start()
    {
        _animator.SetInteger("arms", 23);
        Debug.Log("Старт");
    }


}
