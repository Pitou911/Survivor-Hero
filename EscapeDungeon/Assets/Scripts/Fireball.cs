using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] AudioClip fireballFSX;
    Rigidbody2D myRigidbody;
    float xSpeed;

    public void SetDirection(float direction)
    {
        xSpeed = bulletSpeed * direction;
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(fireballFSX, collision.transform.position);
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);    
    }
}
