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

    public GameObject mistakeText;

    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;
    private InputAction interact;

    private GameObject collisionObject;
    private GameObject triggerObject;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        GameState.Init();
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
        mistakeText.SetActive(false);
        if (GameState.FightState())
        {
            weapon.UseWeapon();
        }
        
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (collisionObject != null)
        {
            if (collisionObject.name == "Home")
            {
                if(GameState.numberOfCats >= 1)
                {
                    collisionObject.GetComponent<Home>().UseHouse();
                }
                
            }      
        }

        if(triggerObject != null)
        {
            if (triggerObject.name == "GunCatShop")
            {
                if (GameState.catfood >= 100)
                {
                    triggerObject.GetComponent<GunCatShop>().BuyCat();
                }
                else
                {
                    Debug.Log("To expensive");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionObject.name == "Home")
        {
            collisionObject = collision.gameObject;
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisionObject.name == "Home")
        {
            collisionObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Cat")
        {
            col.GetComponent<Cat>().GrabThatCat();
        } else if (col.gameObject.name == "GunCatShop")
        {
            triggerObject = col.gameObject;
        }
    }
}
