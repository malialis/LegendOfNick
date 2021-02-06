using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    public Vector2 cameraChange;
    //public Vector2 cameraMinChange; if you have a different size
    //public Vector2 cameraMaxChange;
    public Vector3 playerChange;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    private CameraMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
            {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;

            //cam.minPosition += cameraMinChange;
            //cam.maxPosition += cameraMaxChange;

            other.transform.position += playerChange;
            StartCoroutine(RoomChangeWalkDelay(other));
            if (needText)
            {
                StartCoroutine(PlaceNameCoroutine());
            }
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

}
