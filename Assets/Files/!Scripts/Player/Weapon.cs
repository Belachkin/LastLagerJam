using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour , IInteractable
{

    [SerializeField] private WeaponData _weaponData;

    public WeaponData WeaponData { get => _weaponData; set => _weaponData = value; }
}


