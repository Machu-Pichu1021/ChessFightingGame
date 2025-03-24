using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float health = 100;
    [SerializeField] private float speed = 5;

    private bool shielding;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        InputAction leftStick = playerInput.actions["Left Stick"];
        InputAction lightAttack = playerInput.actions["Attack Button"];
        InputAction specialAttack = playerInput.actions["Special Button"];
        InputAction shield = playerInput.actions["Shield"];

        if (shield.WasPerformedThisFrame())
            shielding = true;
        if (shield.WasReleasedThisFrame())
            shielding = false;

        if (shielding)
        {
            //print("Shielding");
            GameManager.instance.Chess();
        }
        else
        {
            Vector2 stickInput = leftStick.ReadValue<Vector2>();
            rb.velocity = new Vector2(stickInput.x * speed, rb.velocity.y);
        }
    }

    public void CreateHitbox(Vector2 position, float radius, float damage)
    {

    }

    public void OnHit(float damage)
    {
        health -= damage;
    }
}
