using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text text;
    private Controls player;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        player = FindObjectOfType<Controls>();
    }

    void Update()
    {
        if (player.crystals == 61)
        {
            text.fontSize = 100;
            text.text = "WELL DONE";
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 100f);
        }
        else text.text = "Crystals: " + player.crystals;
    }
}