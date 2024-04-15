using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Subscription<PlateEvent> plate_event_sub;

    public int neededPlates = 0;
    public bool finalLevel = false;
    private int currentPlates = 0;
    private bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPlates = 0;
    }

    private void OnEnable()
    {
        plate_event_sub = EventBus.Subscribe<PlateEvent>(_OnPlateUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(plate_event_sub);
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            currentPlates = 0;
        }

        if(currentPlates == neededPlates)
        {
            if (finalLevel)
            {
                Destroy(PlayMusic.instance.gameObject);
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                StartCoroutine(Progress());
            }
        }
    }

    void _OnPlateUpdate(PlateEvent e)
    {
        currentPlates += e.num;
        reset = e.reset;

        Debug.Log("current plates: " + currentPlates);
    }

    public IEnumerator Progress()
    {


        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield return null;
    }
}
