using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectable collectable = collision.GetComponent<Collectable>();
        if(collectable != null)
        {
            collectable.Collect();
        }
    }
}
