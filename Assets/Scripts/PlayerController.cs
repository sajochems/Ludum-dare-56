using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 1.5f;
    public PlayerInputActions playerControls;
    public Weapon weapon;

    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;
    private InputAction interact;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }


    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (GameState.FightState())
        {
            //What happens on click in the fighting state
            weapon.UseWeapon();

        } else if(GameState.BuildState())
        {
            //what happens on click in the building state
        }
             
    }

    private void Interact(InputAction.CallbackContext context)
    {
        GameState.SwitchState();
        if (GameState.FightState())
        {
            //What happens on interact in the fighting state
            
        }
        else if (GameState.BuildState())
        {
            //what happens on interact in the building state
        }
    }
}
