using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoomConfig : DungeonRoomConfig
{
    public Doors[] doors;
    public SignalListeners enemyUpdate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length -1)
            {
                return;
            }
        }
        OpenAllDoors();
    }

    public void CloseAllDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].CloseDoor();
        }
    }

    public void OpenAllDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].OpenDoor();
        }
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
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
        CloseAllDoors();
    }

    public override void OnTriggerExit2D(Collider2D other)
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
                ChangeActivation(breakables[j], false);
            }
            virtualCamera.SetActive(false);
        }
    }

}
