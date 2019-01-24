using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Ground : MonoBehaviour {

    private Transform start;
    float x;
    float y;
    private bool tr = true;
    float speed=1.5f;
    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
    }

    void Update()
    {
        if (tr == false)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            speed += 0.08f;
        }
        else speed = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
            StartCoroutine("respawndelay2");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
    public IEnumerator respawndelay2()
    {
        yield return new WaitForSeconds(0.2f);
        tr = false;
        yield return new WaitForSeconds(5f);
        transform.position = new Vector3(x, y, 0f);
        tr = true;
        
    }
}
