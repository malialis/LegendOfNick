using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMan : Enemy
{
    [Header("Logman Target Attributes")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
   // public Transform homePosition;

    [Header("Logman components")]
    public Rigidbody2D myRigidBody;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("WakeUp", true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 tempPosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeAnimation(tempPosition - transform.position);
                myRigidBody.MovePosition(tempPosition);                
                ChangeState(EnemyState.walk);
                animator.SetBool("WakeUp", true);
            }
            
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            //ChangeState(EnemyState.idle);
            animator.SetBool("WakeUp", false);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

    public void ChangeAnimation(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimationFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                SetAnimationFloat(Vector2.left);
            }

        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                SetAnimationFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimationFloat(Vector2.down);
            }

        }
    }

    private void SetAnimationFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

}
