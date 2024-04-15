using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collisions
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {

            //support for pressure plate may reimplement
            /*float platePos = this.transform.position.x;

            float NPCPos = collision.transform.position.x;

            (NPCPos > (platePos - 1)) && (NPCPos < (platePos + 1)) && */

            if (!active && (collision.gameObject.GetComponent<InfectedMovement>().enabled == true))
            {
                EventBus.Publish<PlateEvent>(new PlateEvent(1, false));
                active = true;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);

                Debug.Log("on plate");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC") && active)
        {
            EventBus.Publish<PlateEvent>(new PlateEvent(-1, false));
            active = false;
            Debug.Log("off plate");

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);

        }
    }
}
