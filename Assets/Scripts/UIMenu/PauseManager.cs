using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public bool usingPausePanel;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public string mainMenu;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        usingPausePanel = false;
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChangePauseState();
        //SwitchPanels();
    }

   /* public void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        ChangePauseState();
    }
   */

    public void ChangePauseState()
    {
        if (isPaused)
        {
            pausePanel.SetActive(true);
            //inventoryPanel.SetActive(false);
            usingPausePanel = true;
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            inventoryPanel.SetActive(false);
            //usingPausePanel = false;
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
        ChangePauseState();
    }

    public void QuitToDesktop()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void GoToInventory()
    {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(true);
    }

    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }


}
