using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFighter : Fighter
{
    public override void NeutralB()
    {
        animator.Play("Neutral B");
    }

    public override void SideB()
    {
        animator.Play("Side B");
    }
    public void StartDash()
    {
        StartCoroutine(Dash(5, 1, 2 / 15f));
    }


    public override void UpB()
    {
        animator.Play("Up B");
    }
    public void StartAscent()
    {
        StartCoroutine(Ascend());
    }
    public void StartDashUpB()
    {
        StartCoroutine(Dash(6.5f, 2, 2 / 3f));
    }
    public void StartDescent()
    {
        StartCoroutine(Descend());
    }


    public override void DownB()
    {
        animator.Play("Down B");
    }

    private IEnumerator Dash(float distance, float speedModifier, float time)
    {
        Vector2 startPos = transform.position;
        float endX = Mathf.Clamp(transform.position.x + (-transform.right.x * distance), -8.5f, 8.5f);
        Vector2 endPos = new Vector2(endX, transform.position.y);
        rb.gravityScale = 0;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime * speedModifier;
            transform.position = Vector2.Lerp(startPos, endPos, t / time);
            yield return null;
        }
        rb.gravityScale = 1;
    }

    private IEnumerator Ascend()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, 1.25f);
        float time = 0.2f;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, t / time);
            yield return null;
        }
        transform.position = endPos;
    }

    private IEnumerator Descend()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(transform.position.x, -3.4f);
        float time = 1 / 3f;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, t / time);
            yield return null;
        }
        transform.position = endPos;
    }
}
