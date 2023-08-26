using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Animator _animator;

    private float horizontal;
    private float vertical;
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            _animator.SetInteger("legs", 1);
        }
        else
        {
            _animator.SetInteger("legs", 5);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(horizontal * _horizontalSpeed, _rigidbody.velocity.y, vertical * _horizontalSpeed);
        
    }

    
}
