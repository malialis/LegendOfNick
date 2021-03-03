using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnockBack : MonoBehaviour
{
    [SerializeField]
    private float thrust;
    [SerializeField]
    private float knockbackTime;
    [SerializeField]
    private string otherTag;
    //public float damage;


    private void OnTriggerEnter2D(Collider2D other)
    {
       /* if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player") && other.GetComponent<Breakable>())
        {
            other.GetComponent<Breakable>().Smash();
        }
       */
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if(hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.DOMove(hit.transform.position + difference, knockbackTime);
                //hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockbackTime);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if(other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponentInParent<PlayerMovement>().Knock(knockbackTime);
                    }                    
                }
                
            }
        }
    }

    

}
