using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedMovement : MonoBehaviour
{
    Subscription<HorizontalEvent> x_event_sub;
    Subscription<VerticalEvent> y_event_sub;

    public float movement_speed = 4f;

    private float horizontal;

    protected Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        x_event_sub = EventBus.Subscribe<HorizontalEvent>(_OnHorizontalUpdate);
        y_event_sub = EventBus.Subscribe<VerticalEvent>(_OnVerticalUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(x_event_sub);
        EventBus.Unsubscribe(y_event_sub);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVelocity = rb.velocity;
        newVelocity = new Vector2(horizontal * movement_speed, rb.velocity.y);

        rb.velocity = newVelocity;
    }

    void _OnHorizontalUpdate(HorizontalEvent e)
    {
        horizontal = e.new_x;
    }

    void _OnVerticalUpdate(VerticalEvent e)
    {
        if(IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, e.new_y);
        }
    }


    bool IsGrounded()
    {
        Collider col = this.GetComponent<BoxCollider>();

        Ray ray = new Ray(col.bounds.center, Vector3.down);

        float radius = col.bounds.extents.x - 0.05f;

        float fullDistance = col.bounds.extents.y + 0.05f;

        if (Physics.SphereCast(ray, radius, fullDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
