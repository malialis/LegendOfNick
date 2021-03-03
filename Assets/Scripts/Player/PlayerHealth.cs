using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    [SerializeField]
    private SignalSender healthSignal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(float ammountToDamage)
    {
        base.TakeDamage(ammountToDamage);
        maxHealth.RuntimeValue = currentHealth;
        healthSignal.Raise();
    }


}
