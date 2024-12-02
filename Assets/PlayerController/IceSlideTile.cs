using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlideTile : MonoBehaviour
{
    public float slideSpeed = 5f; // Speed of sliding

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Ensure only the player interacts with the ice tiles
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Determine the direction the player is sliding
                Vector2 slideDirection = GetSlideDirection(collision.transform.position);

                // Apply sliding force
                rb.velocity = slideDirection * slideSpeed;
            }
        }
    }

    private Vector2 GetSlideDirection(Vector3 position)
    {
        
        return Vector2.right; 
    }
}