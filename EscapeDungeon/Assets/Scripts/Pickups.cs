using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] AudioClip applePickupSFX;
    [SerializeField] int pointsForPickup = 100;

    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForPickup);
            AudioSource.PlayClipAtPoint(applePickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
