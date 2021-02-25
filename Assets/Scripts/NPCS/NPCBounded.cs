using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBounded : Signs

{
    [Header("Movement Related")]
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    public float minMoveTime;
    public float maxMoveTime;
    public float minWaitTime;
    public float maxWaitTime;

    [Header("Component Related")]
    public Collider2D bounds;
    private Rigidbody2D myRigidBody;
    private Animator animator;
    private ThePlayerInput playerInput;
    
    private bool canMove = true;
    private bool isMoving;
    private float moveTimeSeconds;
    private float waitTimeSeconds;



    // Start is called before the first frame update
    void Start()
    {
        //GameEvents.current.OnSubmit += OnSubmit;
        GameEvents.current.OnTalk += OnTalk;
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
        canMove = true;
        ChangeDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;                
            }
            if (!playerInRange)
            {
                Move();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if(waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                isMoving = true;
                waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
            }
        }
    }


    private void ChangeDirection()
    {
        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down;
                break;                
            default:
                break;
        }
        UpdateAnimation();

    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;

        if (bounds.bounds.Contains(temp))
        {
            myRigidBody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
        
    }

    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        int loops = 0;
        ChangeDirection();
        while (temp == directionVector && loops < 100)
        {
            ChangeDirection();
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("moveX", directionVector.x);
        animator.SetFloat("moveY", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }

    public override void DoOnTalk()
    {
        if (playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                
                dialogBox.SetActive(false);
            }
            else
            {
                Debug.Log("I am TALKING to someone yo");
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }

    }


}
