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
            
        }
               
    }

    private void SetRandomHit()
    {
               
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {

        
        switch (Random.Range(0, 2))
        {
            case 0:
                
                _animator.SetInteger("arms", 15);

                yield return new WaitForSeconds(0.5f);

                _animator.SetInteger("arms", 13);
                break;
            case 1:
                
                _animator.SetInteger("arms", 14);

                yield return new WaitForSeconds(0.5f);

                _animator.SetInteger("arms", 13);
                break;

        }
    }
}
