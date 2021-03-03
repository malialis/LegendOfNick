using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Bools yo")]
    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    public PlayerStats[] playerStats;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive)
        {
            Debug.Log("Do this yo");
        }
    }
}
