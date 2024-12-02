using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 3f;
    public LayerMask solidobjectLayer;
    public LayerMask grassLayer;
    public LayerMask iceSurface;

    public LayerMask coinLayer;


    private Vector2 input;
    private bool isMoving;

    public CoinManager cm;

    public event Action OnEncountered;


    private Rigidbody2D rb;
    private bool isSliding;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isSliding)
        {
            HandleUpdate();
        }
        else
        {
            CheckifIce();
        }
    }

    public void HandleUpdate()
    {
        // Movement controls (Grid-based movement)
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Prevent diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        // Check for encounters after moving
        CheckForEncounters();

        // Check if on ice after moving
        CheckifIce();
        // check if coin pickup
        CheckifCoin();

    }

    private bool IsWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidobjectLayer) == null;
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                OnEncountered?.Invoke();
            }
        }
    }

    private void CheckifIce()
    {
        // Check if the player is on ice and start sliding
        if (Physics2D.OverlapCircle(transform.position, 0.2f, iceSurface) != null)
        {
            if (!isSliding)
            {
                isSliding = true;
                Vector2 slideDirection = GetSlideDirection();
                StartCoroutine(SlideOnIce(slideDirection));
            }
        }
        else
        {
            // Stop sliding when leaving ice
            isSliding = false;
        }
    }

    private IEnumerator SlideOnIce(Vector2 slideDirection)
    {
        while (isSliding)
        {
            var targetPos = transform.position + (Vector3)slideDirection;

            // Stop sliding if the next position is not walkable
            if (!IsWalkable(targetPos))
            {
                isSliding = false;
                yield break;
            }

            // Move the player
            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;

            // Pause briefly to simulate sliding motion
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector2 GetSlideDirection()
    {
        // Get the direction based on the last input or current velocity
        if (input.x != 0)
            return new Vector2(Mathf.Sign(input.x), 0);
        else if (input.y != 0)
            return new Vector2(0, Mathf.Sign(input.y));

        // Default to right if no input detected
        return Vector2.right;
    }

   private void CheckifCoin()
    {
     if (Physics2D.OverlapCircle(transform.position, 0.2f, coinLayer) != null)
        {
            cm.coinCount++; // Increment the coin count

        // Deactivate the background layer
        GameObject Coin = GameObject.Find("Coin"); // Find the background object by name
         if (Coin != null)
         {
            Coin.SetActive(false); // Disable the background layer
         }
         else
         {
            Debug.LogWarning("BackgroundLayer not found in the scene!");
         }
        }
    }
}