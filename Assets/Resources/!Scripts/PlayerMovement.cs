using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Transform _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private Rigidbody _rb;

    private float horizontal;
    private float vertical;
    private void Update()
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
        Move();
        
    }

    private void Move()
    {
        
        Vector3 directionHorizontal = new Vector3(horizontal , 0, 0);
        Vector3 directionVertical = new Vector3(0, 0, vertical);

        float magintudeHorizontal = Mathf.Clamp01(directionHorizontal.magnitude) * _horizontalSpeed;
        float magintudeVertical = Mathf.Clamp01(directionVertical.magnitude) * _verticalSpeed;

        directionHorizontal.Normalize();
        directionVertical.Normalize();

        transform.Translate(new Vector3(directionHorizontal.x * magintudeHorizontal * Time.deltaTime, 0, directionVertical.z * magintudeVertical * Time.deltaTime));


        float angle = Vector3.SignedAngle(Vector3.left, (directionHorizontal + directionVertical).normalized, Vector3.up);

        _player.rotation = Quaternion.Euler(0, angle, 0);
        
    }

}

