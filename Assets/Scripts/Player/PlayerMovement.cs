using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    walk,
    attack,
    death,
    stagger,
    idle,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;

    [Header("Player Atrributes")]    
    public float speed;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPosition;

    private Rigidbody2D myRigidBody;
    private Animator animator;
    private Vector3 change;
    private float inputX;
    private float inputY;
    private ThePlayerInput playerInput;

    [Header("Player Scriptables")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public SignalSender playerHit;

    private void Awake()
    {        
        playerInput = new ThePlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentState = PlayerState.walk;
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        //is the player in an interaction
        if(currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = inputX;        
        change.y = inputY;

      // if(Keyboard.current.spaceKey.wasPressedThisFrame && currentState != PlayerState.attack && currentState != PlayerState.stagger)


    }

    private void FixedUpdate()
    {
        if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMovement();
        }
    }

    private void MoveCharacter()
    {
        change.Normalize();

        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    private void UpdateAnimationAndMovement()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private IEnumerator KnockCoroutine(float knockbackTime)
    {
        playerHit.Raise();
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

    public void Knock(float knockbackTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            //playerHealthSignal.Raise();
            StartCoroutine(KnockCoroutine(knockbackTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    public void RaiseItem()
    {
        if(playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.walk;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }        
        
    }

    #region Attacks

    private IEnumerator AttackCoroutine()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        yield return null;
        animator.SetBool("attacking", false);
        Debug.Log("I have struck something");
        yield return new WaitForSeconds(0.25f);
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        
    }

    private IEnumerator SecondaryAttackCoroutine()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.25f);
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        
    }


    #endregion


    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(SecondaryAttackCoroutine());
        }
    }

    

}
