using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConfig : MonoBehaviour
{
    public Enemy[] enemies;
    public Breakable[] breakables;
    public GameObject virtualCamera;


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            //activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int j = 0; j < breakables.Length; j++)
            {
                ChangeActivation(breakables[j], true);
            }
            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactivate all enemies and pots
            //activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int j = 0; j < breakables.Length; j++)
            {
                ChangeActivation(breakables[j], false );
            }
            virtualCamera.SetActive(false);
        }
    }

    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

}
