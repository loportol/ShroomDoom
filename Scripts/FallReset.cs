using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallReset : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            //reset the level manager
            EventBus.Publish<PlateEvent>(new PlateEvent(0, true));

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
