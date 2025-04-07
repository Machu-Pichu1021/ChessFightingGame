using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnHeadProjectile : Hitbox
{
    private float startX;
    private bool returning;
    private int direction = -1;

    private void Start()
    {
        startX = transform.position.x;
    }

    private void Update()
    {
        if (!returning && Mathf.Abs(transform.position.x - startX) > 7.5f)
        {
            direction = 1;
            returning = true;
            launchVelocity = new Vector2(-launchVelocity.x, launchVelocity.y);
            coll.isTrigger = false;
        }
        transform.Translate(10 * direction * Time.deltaTime * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Fighter fighter))
        {
            if (fighter.Equals(owner))
            {
                PawnFighter pawn = (PawnFighter)owner;
                pawn.OnHeadReturn();
                Destroy(gameObject);
            }
            else
            {
                fighter.OnHit(damage, shieldDamage, hitstun, launchVelocity);
                coll.isTrigger = true;
            }
        }
        else
        {
            direction = 1;
            returning = true;
            launchVelocity = new Vector2(-launchVelocity.x, launchVelocity.y);
            coll.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Fighter fighter) && fighter.Equals(owner))
        {
            PawnFighter pawn = (PawnFighter)owner;
            pawn.OnHeadReturn();
            Destroy(gameObject);
        }
    }
}
