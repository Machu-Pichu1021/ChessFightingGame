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
    [Tooltip("The time it takes in seconds for the fighter to reach its max speed.")]
    [SerializeField] private float accelerationTime = 1;
    private float t;

    private float hitstun;

    private int jabIndex;
    [Tooltip("The time in seconds before the jab combo resets between each jab hit")]
    [SerializeField] private float jabTime = 1;
    private float jabTimer;
    [Tooltip("Number of jabs in this fighter's jab combo")]
    [SerializeField] private int maxJabs = 3;

    private int stickFlickFrames = 7;
    private float stickFlickTime;
    private bool chargingSmash;

    private float shieldHealth = 100;
    [SerializeField] private GameObject shieldPrefab;
    private GameObject shieldReference;
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
        {
            shielding = true;
            shieldReference = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }
        else if (shield.WasReleasedThisFrame())
        {
            shielding = false;
            Destroy(shieldReference);
        }
        
        if (hitstun <= 0)
        {
            Vector2 stickInput = leftStick.ReadValue<Vector2>();

            if (stickFlickTime == 0 && Mathf.Abs(stickInput.x) > 0.5f)
                stickFlickTime = Time.time;
            else if (Mathf.Abs(stickInput.x) < 0.5f)
                stickFlickTime = 0;

            if (shielding)
            {
                shieldReference.transform.localScale = Vector2.one * (shieldHealth / 62.5f);
                shieldHealth -= Time.deltaTime * 10;
                print("Shielding");
                if (Mathf.Abs(stickInput.x) > 0.5f)
                    print("Roll");
                else if (stickInput.y < -0.5f)
                    print("Spot dodge");
            }
            else if (chargingSmash)
            {
                print("Charging smash");
                if (lightAttack.WasReleasedThisFrame())
                {
                    stickFlickTime = 0;
                    chargingSmash = false;
                    if (stickInput.y > 0.5f)
                        print("Smash attack Angled Up");
                    else if (stickInput.y < -0.5f)
                        print("Smash attack Angled Down");
                    else
                        print("Smash attack Angled Side");
                }
            }
            else
            {
                if (lightAttack.WasPerformedThisFrame())
                {
                    if (Mathf.Abs(stickInput.x) < 0.5f)
                    {
                        print("Jab " + jabIndex);
                        jabIndex++;
                        jabTimer = jabTime;
                        if (jabIndex >= maxJabs)
                        {
                            jabTimer = 0;
                            jabIndex = 0;
                        }
                    }
                    else if (Time.time - stickFlickTime > Time.deltaTime * stickFlickFrames)
                    {
                        if (Mathf.Abs(rb.velocity.x) >= speed * 0.85f)
                            print("Dash attack");
                        else
                            print("Tilt attack");
                    }
                    else
                    {
                        chargingSmash = true;
                    }
                }
                else if (specialAttack.WasPerformedThisFrame())
                {
                    if (Mathf.Abs(stickInput.x) > 0.5f)
                        print("Side Special");
                    else if (stickInput.y > 0.5f)
                        print("Up Special");
                    else if (stickInput.y < -0.5f)
                        print("Down Special");
                    else
                        print("Neutral Special");
                }
                if (Mathf.Abs(stickInput.x) > 0.5f && t < accelerationTime)
                    t += Time.deltaTime;
                else if (Mathf.Abs(stickInput.x) > 0.25f)
                    t = accelerationTime / 2;
                else
                    t = 0;
                Vector2 desiredVelocity = new Vector2(stickInput.x * speed, rb.velocity.y);
                rb.velocity = Vector2.Lerp(rb.velocity, desiredVelocity, t / accelerationTime);
            }
        }

        jabTimer -= Time.deltaTime;
        if (jabTimer <= 0)
            jabIndex = 0;

        hitstun -= Time.deltaTime;
    }

    public void CreateHitbox(Vector2 position, float radius, float damage, float hitstun, Vector2 launchVelocity)
    {

    }

    public void OnHit(float damage, float hitstun, Vector2 launchVelocity)
    {
        rb.velocity = launchVelocity;
        this.hitstun = hitstun;
        health -= damage;
    }
}
