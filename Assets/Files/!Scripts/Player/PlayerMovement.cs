using System;
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
    [SerializeField] private GameObject _smokePrefab;
    private void Update()
    {
        if (!IsDie)
        {
            horizontal = Input.GetAxisRaw("Horizontal") * -1;
            vertical = Input.GetAxisRaw("Vertical") * -1;

            if (horizontal != 0 || vertical != 0)
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

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        _rb.velocity = direction * _speed * Time.fixedDeltaTime + new Vector3(0, _rb.velocity.y, 0);

        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(Vector3.left, direction, Vector3.up);

            _player.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public void FinishLevel(Transform endPos)
    {      
        StartCoroutine(FinishAnimations(endPos));
    }

    IEnumerator FinishAnimations(Transform endPos)
    {
             
        Vector3 currentAngle = _player.transform.eulerAngles;

        currentAngle = new Vector3(Mathf.LerpAngle(currentAngle.x, endPos.eulerAngles.x, 10), Mathf.LerpAngle(currentAngle.y, endPos.eulerAngles.y, 10), Mathf.LerpAngle(currentAngle.z, endPos.eulerAngles.z, 10f));

        _player.transform.eulerAngles = currentAngle;

        _player.transform.position = new Vector3(endPos.position.x, endPos.position.y, endPos.position.z);

        _animator.SetInteger("legs", 5);
        yield return new WaitForSeconds(0.5f);

        _smokePrefab.SetActive(true);

        _animator.SetInteger("arms", 7);

        AudioManager.instance.Play("smoke");

        yield return new WaitForSeconds(3f);

        AudioManager.instance.Play("buyHueta");
          
    }
}

