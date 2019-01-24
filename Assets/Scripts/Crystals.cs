using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Controls player;

    void Start()
    {
        player = FindObjectOfType<Controls>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.crystals += 0.5f;
            Destroy(gameObject);
        }
    }
}