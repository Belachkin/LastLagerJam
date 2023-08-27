using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] Transform _endPos;
    [SerializeField] GameObject _camera;
    private Animator _animation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerAttack>()._isAttacking = true;
            other.gameObject.GetComponent<PlayerMovement>().IsDie = true;

            other.gameObject.GetComponent<PlayerMovement>().FinishLevel(_endPos);

            _camera.SetActive(true);
        }
    }
}
