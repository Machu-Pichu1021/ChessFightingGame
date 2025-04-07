using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFighter : Fighter
{
    private bool hasCopied;
    private RuntimeAnimatorController copiedController;

    [SerializeField] private GameObject headProjectile;
    [SerializeField] private GameObject pawnBrotherPrefab;
    private GameObject pawnBrother;

    public override void NeutralB()
    {
        if (!hasCopied)
            animator.Play("Neutral B");
        else
        {
            RuntimeAnimatorController currentController = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = copiedController;
            animator.Play("Neutral B");
            animator.runtimeAnimatorController = currentController;
        }
    }

    public void Copy()
    {
        Fighter opponent = Array.Find(FindObjectsByType<Fighter>(FindObjectsSortMode.None), fighter => !fighter.Equals(this));
        if (opponent is PawnFighter)
            meter += 25;
        else
        {
            hasCopied = true;
            copiedController = opponent.GetComponent<Animator>().runtimeAnimatorController;
        }
    }

    public override void SideB()
    {
        animator.Play("Side B");
    }

    public void BrainBlast()
    {
        PawnHeadProjectile projectile = Instantiate(headProjectile, hitboxTransform.position, transform.rotation).GetComponent<PawnHeadProjectile>();
        projectile.Initialize(moveDamage, moveShieldDamage, moveRadius, moveHitstun, moveLaunchVelocity, moveLifetime, this);
    }

    public void OnHeadReturn()
    {
        animator.Play("Reverse Side B");
    }

    public override void UpB()
    {
        if (pawnBrother == null)
            animator.Play("Up B");
    }

    public void SummonBrother()
    {
        pawnBrother = Instantiate(pawnBrotherPrefab, hitboxTransform.position, transform.rotation);
    }

    public override void DownB()
    {
        throw new System.NotImplementedException();
    }
}
