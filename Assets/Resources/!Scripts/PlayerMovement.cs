using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    //[SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Animator _animator;

    private float horizontal;
    private float vertical;
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * -1;
        vertical = Input.GetAxisRaw("Vertical") * -1;

        if(horizontal != 0 || vertical != 0)
        {
            _animator.SetInteger("legs", 1);
            //_animator.SetInteger("arms", 13);
        }
        else
        {
            _animator.SetInteger("legs", 5);
            
        }
        Move();
    }

    private void Move()
    {
        Vector3 directionHorizontal = new Vector3(horizontal, 0, 0);
        Vector3 directionVertical = new Vector3(0, 0, vertical);

        float magintudeHorizontal = Mathf.Clamp01(directionHorizontal.magnitude) * _horizontalSpeed;
        float magintudeVertical = Mathf.Clamp01(directionVertical.magnitude) * _verticalSpeed;

        directionHorizontal.Normalize();
        directionVertical.Normalize();

        transform.Translate(new Vector3(directionHorizontal.x * magintudeHorizontal * Time.deltaTime, 0, directionVertical.z * magintudeVertical * Time.deltaTime));
    }
}
