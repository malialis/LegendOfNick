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
    public SignalSender reduceMagic;
    public float magicCost;
    public MagicManager magicManager;

    [Header("Projectile things")]
    public Item bow;
    public GameObject projectile;
    public Transform spawnPoint;

    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;


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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
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
            StartCoroutine(FlashCoroutine());
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

    private IEnumerator FlashCoroutine()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);            
            temp++;

        }
        triggerCollider.enabled = true;
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

    private IEnumerator SecondWeaponAttackCoroutine()
    {
        //animator.SetBool("SecondWeapon", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("SecondWeapon", false);
        yield return new WaitForSeconds(0.25f);
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        
    }

    private void MakeArrow()
    {
        if(playerInventory.currentMagic > 0)
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY")); // get the current direction of player standin
            ArrowProjectile arrow = Instantiate(projectile, spawnPoint.position, Quaternion.identity).GetComponent<ArrowProjectile>();
            arrow.Setup(temp, ChooseArrowDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
           // magicCost = projectile.GetComponent<PlayerProjectiles>().magicCost;
            // magicManager.DecreaseMagic(magicCost);
            reduceMagic.Raise();
        }        
        
    }

    private Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    #endregion

    #region new Input System

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

    public void Shoot(InputAction.CallbackContext context)
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if (playerInventory.CheckForItem(bow))
            {
                StartCoroutine(SecondWeaponAttackCoroutine());
            }            
        }
    }

    public void Submit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEvents.current.DoSubmit();
            //Debug.Log("I am interacting");
        }
        
    }

    public void ReadAndTalk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEvents.current.DoRead();
            //Debug.Log("I am interacting");
        }

    }

    public void BeginTalking(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEvents.current.DoTalk();
            Debug.Log("I am interacting by Talking");
        }

    }


    public void Dash(InputAction.CallbackContext context)
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(SecondWeaponAttackCoroutine());
        }
    }

    #endregion


}
