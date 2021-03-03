using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class DamageSystem : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private string otherTag;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            HealthSystem temp = other.GetComponent<HealthSystem>();
            if (temp)
            {
                temp.TakeDamage(damage);
            }
        }
    }



}
