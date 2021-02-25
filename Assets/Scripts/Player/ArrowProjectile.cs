using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : PlayerProjectiles
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public float lifeTime;
    private float lifeTimeCounter;
    
    public float impactDestroyDelay;
    public GameObject impactFX;
    //public float magicCost;


    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
    }

    private void Update()
    {
        lifeTimeCounter -= Time.deltaTime;
        if(lifeTimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine("DeathFX");
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine("SelfDestruct");
        }
        
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine("DeathFX");
        Destroy(this.gameObject);
    }

    private IEnumerator DeathFX()
    {
        GameObject effect = Instantiate(impactFX, transform.position, Quaternion.identity);
        Destroy(effect, impactDestroyDelay);
        Debug.Log("SMOKE!!!!");
        yield return new WaitForSeconds(.25f);
    }
}
