using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private PlayerAttack _playerAttack;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Interact(interactable);

            AudioManager.instance.Play("takeItem");

            Destroy(other.gameObject);
        }
    }

    private void Interact(IInteractable interactable)
    {
        _playerAttack.UseCount = interactable.WeaponData.UseCount;
        _playerAttack.IdleAnimation = interactable.WeaponData.idleAnimation;
        _playerAttack.WeaponAnimations = interactable.WeaponData.AttackAnimationNumber;
        _playerAttack.Damage = interactable.WeaponData.Damage;
        _playerAttack.AnimationTime = interactable.WeaponData.AnimationTime;
        _playerAttack.isArm = false;
        _playerAttack.ChangeWeapon(interactable.WeaponData.Name);
    } 
}
