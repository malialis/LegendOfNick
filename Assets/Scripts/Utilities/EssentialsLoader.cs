using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject hud;
    public GameObject gameManager;
    public GameObject thePlayer;
    public GameObject gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(hud == null)
        {
            Instantiate(hud);
        }

        if(thePlayer == null)
        {
            Instantiate(thePlayer);
        }

        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        
    }

}
