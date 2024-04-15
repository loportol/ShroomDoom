using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoverment : MonoBehaviour
{
    protected Rigidbody rb;

    public float movementSpeed = 4;
    public float jumpPower = 15;
    public MushroomInput playerControls;

    private float horizontal;
    private InputAction move;
    private InputAction jump;

    private Animator playerAnimator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new MushroomInput();
    }

    private void Start()
    {
        //make sure the controller is conneccted
        for(int i = 0; i < Gamepad.all.Count; ++i)
        {
            Debug.Log(Gamepad.all[i].name);
        }

        playerAnimator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        move.performed += Move;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Constantly send the horizontal position
        //EventBus 
        EventBus.Publish<HorizontalEvent>(new HorizontalEvent(horizontal));

        Vector3 newVelocity = rb.velocity;
        //Horizontal
        newVelocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        rb.velocity = newVelocity;

        EventBus.Publish<PositionEvent>(new PositionEvent(transform.position));

        playerAnimator.SetBool("IsJumping", !IsGrounded());
        playerAnimator.SetBool("IsWalking", IsMoving());

        //Flip sprite
        bool flipped = horizontal < 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 0f : 180f, 0f));
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        //Jump and keep the horizontal momentum 
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            EventBus.Publish<VerticalEvent>(new VerticalEvent(jumpPower));
        }
    }

    bool IsGrounded()
    {
        Collider col = this.GetComponent<CapsuleCollider>();

        Ray ray = new Ray(col.bounds.center, Vector3.down);

        float radius = col.bounds.extents.x - 0.05f;

        float fullDistance = col.bounds.extents.y + 0.05f;

        if(Physics.SphereCast(ray, radius, fullDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsMoving()
    {
        if(horizontal != 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
