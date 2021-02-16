using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsNSwitches : MonoBehaviour
{
    public bool isActive;
    public BoolValue storedValue;

    public Sprite activeSprite;
    private SpriteRenderer mySprite;

    public Doors thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        isActive = storedValue.RuntimeValue;
        if (isActive)
        {
            ActivateSwitch();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //is it the player you are looking for
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
        
    }

    public void ActivateSwitch()
    {
        isActive = true;
        storedValue.RuntimeValue = isActive;
        thisDoor.OpenDoor();
        mySprite.sprite = activeSprite;
    }

}
