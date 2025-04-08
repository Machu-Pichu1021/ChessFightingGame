using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopSlash : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().Play("Slash");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
