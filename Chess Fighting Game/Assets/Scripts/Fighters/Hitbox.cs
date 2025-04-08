using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    protected CircleCollider2D coll;
    protected float damage;
    protected float shieldDamage;
    protected float hitstun;
    protected Vector2 launchVelocity;
    protected float lifetime;
    protected Fighter owner;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    public void Initialize(float dmg, float shieldDmg, float radius, float hitstun, Vector2 launchVelocity, float lifetime, Fighter owner)
    {
        damage = dmg;
        shieldDamage = shieldDmg;
        coll.radius = radius;
        this.hitstun = hitstun;
        this.launchVelocity = launchVelocity;
        this.lifetime = lifetime;
        this.owner = owner;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Fighter fighter) && !fighter.Equals(owner))
        {
            fighter.OnHit(damage, shieldDamage, hitstun, launchVelocity);
            Destroy(gameObject);
        }
    }
}
