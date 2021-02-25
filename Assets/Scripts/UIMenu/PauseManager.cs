using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;


    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangePauseState();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        ChangePauseState();
    }

    public void ChangePauseState()
    {
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
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


}
