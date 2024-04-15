using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfectScript : MonoBehaviour
{
    public MushroomInput playerControls;
    public GameObject radius;
    public GameObject sporePrefab;

    private InputAction infect;
    private InputAction cure;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new MushroomInput();
    }

    private void OnEnable()
    {
        infect = playerControls.Player.Infect;
        infect.Enable();
        infect.performed += Infect;

        cure = playerControls.Player.Cure;
        cure.Enable();
        cure.performed += Cure;
    }

    private void OnDisable()
    {
        infect.Disable();
        cure.Disable();
    }

    public void Infect(InputAction.CallbackContext context)
    {
        if (context.performed)
        { 
            StartCoroutine(RadiusEnable());
        }
    }

    public void Cure(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventBus.Publish<CureEvent>(new CureEvent(false));
        }
    }

    public IEnumerator RadiusEnable()
    {
        Instantiate(sporePrefab, transform.position, Quaternion.identity, transform);

        radius.SetActive(true);
        yield return new WaitForSeconds(.1f);
        radius.SetActive(false);

        yield return null;
    }

}
