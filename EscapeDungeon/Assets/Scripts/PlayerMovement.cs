using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform skill;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    SpriteRenderer mySprite;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        myAnimator = gameObject.GetComponent<Animator>();
        myBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        myFeetCollider = gameObject.GetComponent<BoxCollider2D>();  
    }

    void Update()
    {
        if (!isAlive){ return; }
        Run();
        FlipSprite();
        Die();
    }
    void OnMove(InputValue inputValue)
    {
        if (!isAlive) { return; }
        moveInput = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue)
    {
        if (!isAlive) { return; }
        if (inputValue.isPressed)
        {
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }
            // do stuff
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue inputValue)
    {
        if (!isAlive) { return; }
        //Instantiate(fireball, skill.position, transform.rotation);
        GameObject newFireball = Instantiate(fireball, skill.position, transform.rotation);

        // Determine the direction based on the player's facing direction
        float direction = mySprite.flipX ? -1f : 1f;

        // Set the direction for the fireball
        newFireball.GetComponent<Fireball>().SetDirection(direction);
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        
        if(playerHasHorizontalSpeed)
        {
            mySprite.flipX = (Mathf.Sign(myRigidbody.velocity.x)) < 0;
        }
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
