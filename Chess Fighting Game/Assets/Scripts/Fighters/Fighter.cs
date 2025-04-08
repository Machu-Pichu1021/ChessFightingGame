using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Fighter : MonoBehaviour
{
    private PlayerInput playerInput;
    protected Rigidbody2D rb;
    protected Animator animator;

    [SerializeField] private float health = 1000;

    [SerializeField] protected float speed = 5;

    private float hitstun;
    [SerializeField] private Transform groundCheckpoint;

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
    private float shieldStun;
    [SerializeField] private GameObject shieldPrefab;
    private GameObject shieldReference;
    private bool shielding;

    [SerializeField] protected float meter;

    [SerializeField] [HideInInspector] private bool isAttacking;

    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] protected Transform hitboxTransform;
    [SerializeField] [HideInInspector] protected float moveRadius;
    [SerializeField] [HideInInspector] protected float moveDamage;
    [SerializeField] [HideInInspector] protected float moveShieldDamage;
    [SerializeField] [HideInInspector] protected float moveHitstun;
    [SerializeField] [HideInInspector] protected Vector2 moveLaunchVelocity = Vector2.zero;
    [SerializeField] [HideInInspector] protected float moveLifetime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        InputAction leftStick = playerInput.actions["Left Stick"];
        InputAction lightAttack = playerInput.actions["Attack Button"];
        InputAction specialAttack = playerInput.actions["Special Button"];
        InputAction shield = playerInput.actions["Shield"];

        bool grounded = IsOnGround();

        if (hitstun <= 0 && shieldStun <= 0 && !isAttacking && grounded)
        {
            if (shield.WasPerformedThisFrame())
            {
                shielding = true;
                shieldReference = Instantiate(shieldPrefab, transform);
            }
            else if (shield.WasReleasedThisFrame())
            {
                shielding = false;
                Destroy(shieldReference);
            }

            Vector2 stickInput = leftStick.ReadValue<Vector2>();
            if (chargingSmash)
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

            if (stickFlickTime == 0 && Mathf.Abs(stickInput.x) > 0.5f)
                stickFlickTime = Time.time;
            else if (Mathf.Abs(stickInput.x) < 0.5f)
                stickFlickTime = 0;

            if (shielding)
            {
                shieldReference.transform.localScale = Vector2.one * (shieldHealth / 250);
                shieldHealth -= Time.deltaTime * 10;
                if (shieldHealth <= 0)
                {
                    shieldStun = 7.5f;
                    shieldHealth = 100;
                    Destroy(shieldReference);
                }
                if (Mathf.Abs(stickInput.x) > 0.5f)
                    print("Roll");
                else if (stickInput.y < -0.5f)
                    print("Spot dodge");
            }
            else if (!chargingSmash) 
            {
                shieldHealth += Time.deltaTime * 15;
                shieldHealth = Mathf.Clamp(shieldHealth, 0, 100);

                if (lightAttack.WasPerformedThisFrame())
                {
                    if (Mathf.Abs(stickInput.x) < 0.5f)
                    {
                        animator.Play("Jab " + jabIndex);
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
                        if (Mathf.Abs(rb.velocity.x) >= speed * 0.75f)
                            animator.Play("Dash Attack");
                        else
                            print("Tilt attack");
                    }
                    else
                        chargingSmash = true;
                }
                else if (specialAttack.WasPerformedThisFrame())
                {
                    if (Mathf.Abs(stickInput.x) > 0.5f)
                        SideB();
                    else if (stickInput.y > 0.5f)
                        UpB();
                    else if (stickInput.y < -0.5f)
                        DownB();
                    else
                        NeutralB();
                }

                rb.velocity = new Vector2(stickInput.x * speed, rb.velocity.y);
                if (rb.velocity.x > 0.1f)
                    animator.transform.rotation = Quaternion.Euler(new Vector2(0, 180));
                else if (rb.velocity.x < -0.1f)
                    animator.transform.rotation = Quaternion.identity;
            }
        }

        jabTimer -= Time.deltaTime;
        if (jabTimer <= 0)
            jabIndex = 0;

        hitstun -= Time.deltaTime;
        shieldStun -= Time.deltaTime;

        animator.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("grounded", grounded);
        animator.SetBool("shielding", shielding);
    }

    public abstract void NeutralB();
    public abstract void SideB();
    public abstract void UpB();
    public abstract void DownB();

    private bool IsOnGround()
    {
        return Physics2D.Raycast(groundCheckpoint.position, Vector2.down, .1f, LayerMask.NameToLayer("Ground"));
    }

    public void CreateHitBox() {
        Hitbox hitbox = Instantiate(hitboxPrefab, hitboxTransform).GetComponent<Hitbox>();
        if (animator.transform.rotation == Quaternion.identity)
            moveLaunchVelocity = new Vector2(-moveLaunchVelocity.x, moveLaunchVelocity.y);
        hitbox.Initialize(moveDamage, moveShieldDamage, moveRadius, moveHitstun, moveLaunchVelocity, moveLifetime, this);
    }

    public void OnHit(float damage, float shieldDamage, float hitstun, Vector2 launchVelocity)
    {
        if (!shielding)
        {
            rb.velocity = launchVelocity;
            this.hitstun = hitstun;
            health -= damage;

            isAttacking = false;
            jabTimer = 0;
            jabIndex = 0;
            shieldStun = 0;
        }
        else
            shieldHealth -= shieldDamage;
    }
}
