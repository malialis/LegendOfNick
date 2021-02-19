using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("Movement Attributes")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("LifeTime Related")]
    public float lifeTime;
    private float lifeTimeSeconds;
    public float impactDestroyDelay;

    public Rigidbody2D myRigidBody;
    public GameObject impactFX;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        lifeTimeSeconds = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeSeconds -= Time.deltaTime;
        if(lifeTimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void LaunchProjectile(Vector2 initialVelocity)
    {
        myRigidBody.velocity = initialVelocity * moveSpeed;
        Debug.Log("FIRE!!!!");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            GameObject effect = Instantiate(impactFX, transform.position, Quaternion.identity);
            Destroy(effect, impactDestroyDelay);
            Debug.Log("SMOKE!!!!");
        }
        
        Destroy(this.gameObject);
    }



}
