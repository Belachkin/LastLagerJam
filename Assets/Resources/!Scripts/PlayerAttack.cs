using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            SetRandomHit();
            Debug.Log("Hit");
        }
        

        //if (Input.GetMouseButtonDown(1))
        //{

        //}
    }

    private void SetRandomHit()
    {
        
        

        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {

        
        switch (Random.Range(0, 3))
        {
            case 0:
                Debug.Log(0);
                _animator.SetInteger("arms", 15);

                yield return new WaitForSeconds(0.5f);

                _animator.SetInteger("arms", 13);
                break;
            case 1:
                Debug.Log(1);
                _animator.SetInteger("arms", 14);

                yield return new WaitForSeconds(0.5f);

                _animator.SetInteger("arms", 13);
                break;
            case 2:
                Debug.Log(2);

                _animator.SetInteger("arms", 15);

                yield return new WaitForSeconds(0.5f);

                _animator.SetInteger("arms", 14);

                yield return new WaitForSeconds(0.5f);

                _animator.SetInteger("arms", 13);

                break;

        }
    }
}
