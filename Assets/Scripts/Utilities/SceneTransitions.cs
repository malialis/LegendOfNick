using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStored;

    public string placeName;
    public GameObject text;
    public Text placeText;

    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWaitTime;
    public GameObject hud;

    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 2f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStored.initialValue = playerPosition;

            StartCoroutine(RoomChangeWalkDelay(other));
            StartCoroutine(PlaceNameCoroutine());
            StartCoroutine(FadeCoroutine());

            //SceneManager.LoadScene(sceneToLoad);                       
        }
    }



    private IEnumerator PlaceNameCoroutine()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

    private IEnumerator RoomChangeWalkDelay(Collider2D other)
    {
        other.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
        yield return new WaitForSeconds(1.25f);

        other.GetComponent<PlayerMovement>().currentState = PlayerState.walk;
    }

    private IEnumerator FadeCoroutine()
    {
        if(fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWaitTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
    }

}


