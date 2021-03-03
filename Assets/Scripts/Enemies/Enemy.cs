using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    searching
}

public class Enemy : MonoBehaviour
{
    [Header("StateMachine")]
    public EnemyState currentState;

    [Header("Enemy Attributes")]
    public float health;
    public FloatValue maxHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePosition;

    [Header("Enemy Prefabs")]
    public GameObject deathFX;
    private float deathDestryDelay = 1.5f;
    public LootTable thisLoot;

    [Header("DeathSignals")]
    public SignalSender roomSignal;

    private void Awake()
    {
        health = maxHealth.initialValue;        
    }

    public void OnEnable()
    {
        transform.position = homePosition;
        health = maxHealth.initialValue;
        currentState = EnemyState.idle;
    }

    public void Knock(Rigidbody2D myRigidbody, float knockbackTime)
    {
        StartCoroutine(KnockCoroutine(myRigidbody, knockbackTime));       
    }

    private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockbackTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DeathEffect();
            MakeLoot();
            if(roomSignal != null)
            {
                roomSignal.Raise();
            }            
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if(deathFX != null)
        {
            GameObject effect = Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(effect, deathDestryDelay);
        }
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            PowerUP current = thisLoot.LootPowerUp();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

}
