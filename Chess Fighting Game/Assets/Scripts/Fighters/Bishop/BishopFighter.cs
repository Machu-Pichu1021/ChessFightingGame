using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopFighter : Fighter
{
    [SerializeField] private GameObject gloveProjectile;
    [SerializeField] private GameObject slashPrefab;

    private bool isPraying;

    public override void NeutralB()
    {
        animator.Play("Neutral B");
    }
    public void SummonGlove()
    {
        BishopGloveProjectile projectile = Instantiate(gloveProjectile, hitboxTransform.position, Quaternion.identity).GetComponent<BishopGloveProjectile>();
        projectile.Initialize(moveDamage, moveShieldDamage, 0.07f, moveHitstun, moveLaunchVelocity, 5, this);
    }

    public override void SideB()
    {
        animator.Play("Side B");
    }
    public void Slash()
    {
        Instantiate(slashPrefab, hitboxTransform.position, Quaternion.identity);
    }

    public override void UpB()
    {
        animator.Play("Up B");
    }
    public void StartTeleport()
    {
        StartCoroutine(Teleport());
    }
    private IEnumerator Teleport()
    {
        speed = 15;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        renderer.enabled = false;
        collider.isTrigger = true;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        speed = 4;
        renderer.enabled = true;
        collider.isTrigger = false;
        rb.gravityScale = 1;
        Return();
    }
    private void Return()
    {
        animator.Play("Reverse Up B");
    }

    public override void DownB()
    {
        if (!isPraying)
            animator.Play("Down B");
        else
            animator.Play("Reverse Down B");
    }
    public void StartPraying()
    {
        isPraying = true;
        animator.Play("Pray");
    }
    public void PrayTick()
    {
        meter += 2.5f;
    }
    public void CancelPray()
    {
        isPraying = false;
    }

    private void LateUpdate()
    {
        animator.SetBool("isPraying", isPraying);
    }
}
