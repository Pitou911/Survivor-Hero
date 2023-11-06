using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidbody;
    SpriteRenderer mySprite;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        mySprite.flipX = (-(Mathf.Sign(myRigidbody.velocity.x))) < 0;
    }
}
