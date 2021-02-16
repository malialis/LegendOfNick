using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPatrol : LogMan
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 tempPosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeAnimation(tempPosition - transform.position);
                myRigidBody.MovePosition(tempPosition);
                ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }

        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            //ChangeState(EnemyState.idle);
            animator.SetBool("WakeUp", false);
        }
    }





}
