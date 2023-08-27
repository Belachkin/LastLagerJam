using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
   

    [SerializeField] private Transform _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private Rigidbody _rb;

    private float horizontal;
    private float vertical;

    public bool IsDie = false;

    private void Update()
    {
        if (!IsDie)
        {
            horizontal = Input.GetAxisRaw("Horizontal") * -1;
            vertical = Input.GetAxisRaw("Vertical") * -1;

            if(horizontal != 0 || vertical != 0)
            {
                _animator.SetInteger("legs", 1);
            }
            else
            {
                _animator.SetInteger("legs", 5);
            
            }
        }

        
    }

    private void FixedUpdate()
    {
        if (!IsDie)
        {
            Move();
        }
        
    }

    private void Move()
    {
        
        Vector3 direction = new Vector3(horizontal , 0, vertical).normalized;
        
        _rb.velocity = direction * _speed * Time.fixedDeltaTime + new Vector3(0, _rb.velocity.y, 0);
        
        if(direction != Vector3.zero) 
        { 
            float angle = Vector3.SignedAngle(Vector3.left, direction, Vector3.up);
            
            _player.rotation = Quaternion.Euler(0, angle, 0);
        }
        
        

        
        
    }

}

