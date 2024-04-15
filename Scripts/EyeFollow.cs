using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    Subscription<PositionEvent> position_event_sub;
    public Vector3 playerpos;

    private void OnEnable()
    {
        position_event_sub = EventBus.Subscribe<PositionEvent>(_OnPositionUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(position_event_sub);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = playerpos - transform.position;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    void _OnPositionUpdate(PositionEvent e)
    {
        playerpos = e.position;
    }
}
