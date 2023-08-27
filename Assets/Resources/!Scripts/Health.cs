using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;
    public float Value;

    private void Start()
    {
        Value = MaxHealth;
    }
}
