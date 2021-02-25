using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : LogMan
{
   

    void FixedUpdate()
    {
        CheckDistance();
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 tempPosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeAnimation(tempPosition - transform.position);
                myRigidBody.MovePosition(tempPosition);
                ChangeState(EnemyState.walk);
                
            }

        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine("AttackingCoroutine");
            }               
        }


    }

    public IEnumerator AttackingCoroutine()
    {
        yield return new WaitForSeconds(.25f);
        currentState = EnemyState.attack;
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1.0f);
        currentState = EnemyState.walk;
        animator.SetBool("isAttacking", false);
    }



}
