using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopGloveProjectile : Hitbox
{
    private int direction = 1;

    public new void Initialize(float dmg, float shieldDmg, float radius, float hitstun, Vector2 launchVelocity, float lifetime, Fighter owner)
    {
        damage = dmg;
        shieldDamage = shieldDmg;
        coll.radius = radius;
        this.hitstun = hitstun;
        this.launchVelocity = launchVelocity;
        this.lifetime = lifetime;
        this.owner = owner;

        Fighter enemy = Array.Find(FindObjectsByType<Fighter>(FindObjectsSortMode.None), fighter => !fighter.Equals(owner));
        if (transform.position.x > enemy.transform.position.x)
            direction = -1;

        Vector2 dir = enemy.transform.position - transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + (180 + direction * 5);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        transform.Translate(17.5f * direction * Time.deltaTime * transform.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Fighter fighter))
        {
            if (!fighter.Equals(owner))
            {
                fighter.OnHit(damage, shieldDamage, hitstun, launchVelocity);
                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }
}
