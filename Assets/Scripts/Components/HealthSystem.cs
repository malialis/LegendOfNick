using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public FloatValue maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.RuntimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Heal(float ammountToHeal)
    {
        currentHealth += ammountToHeal;
        if(currentHealth > maxHealth.RuntimeValue)
        {
            currentHealth = maxHealth.RuntimeValue;
        }
    }

    public virtual void TakeDamage(float ammountToDamage)
    {
        currentHealth -= ammountToDamage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    public virtual void FullHeal()
    {
        currentHealth = maxHealth.RuntimeValue;
    }

    public virtual void InsantDeath()
    {
        currentHealth = 0;
    }



}
