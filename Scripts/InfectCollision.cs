using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectCollision : MonoBehaviour
{
    Subscription<CureEvent> cure_event_sub;
    protected Rigidbody rb;

    SpriteRenderer sprite;

    public Sprite infectSprite;
    public Sprite normalSprite;
    public GameObject eyes;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        cure_event_sub = EventBus.Subscribe<CureEvent>(_OnCureUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(cure_event_sub);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.isKinematic = false;

            this.GetComponent<InfectedMovement>().enabled = true;
            sprite.sprite = infectSprite;
            eyes.SetActive(false);
        }

    }


    void _OnCureUpdate(CureEvent e)
    {
        this.GetComponent<InfectedMovement>().enabled = e.cure;
        rb.isKinematic = true;
        sprite.sprite = normalSprite;
        eyes.SetActive(true);
    }

}
