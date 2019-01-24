using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public bool moveright;
    public bool moveleft;
    public bool jump;
    public float jumpheight;
    public Rigidbody2D rb;
    public float movespeed;
    public float crystals;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Collider2D onGround;
    private Animator anim;
    public GameObject Explode;
    public GameObject Dust;
    private GameObject [] defeatedEnemy;
    public bool damaging = false;
    public bool smash = false;
    private int dftEnm;
    void Start()
    {
        dftEnm = 0;
        defeatedEnemy = new GameObject[50];
        rb = GetComponent<Rigidbody2D>();
        crystals = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x != 0 && onGround)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-movespeed, rb.velocity.y);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(movespeed, rb.velocity.y);

        }

        if (moveright)
        {
            rb.velocity = new Vector2(movespeed, rb.velocity.y);
        }
        if (moveleft)
        {
            rb.velocity = new Vector2(-movespeed, rb.velocity.y);
        }

        if (!moveleft && !moveright && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && rb.velocity.x != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / (1.1f), rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpheight);
        }
        if (jump && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpheight);

        }
        if (Input.GetKey(KeyCode.Space) && !onGround && Mathf.Abs(rb.velocity.y) < 2.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpheight * 2);
            damaging = true;
            anim.SetBool("Smash", true);
        }
        if (smash && !onGround && Mathf.Abs(rb.velocity.y) < 5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpheight * 2);
            damaging = true;
            anim.SetBool("Smash", true);
        }



    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.tag == "StdEnemy" && damaging)
        {
            defeatedEnemy[dftEnm] = other.gameObject;
            Instantiate(Explode, other.transform.position, other.transform.rotation);
            other.enabled = false;
            other.GetComponent<Renderer>().enabled = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpheight * 1.5f);
            dftEnm=(dftEnm+1)%50;
            StartCoroutine("damagedelay");
            anim.SetBool("Smash", false);

        }
       
        if(other.tag == "Ground") {
            anim.SetBool("Smash", false);
            Instantiate(Dust, new Vector3(transform.position.x, transform.position.y - 0.6f, 0f), transform.rotation);
     
            StartCoroutine("damagedelay");
        }
        if (other.tag == "Spykes")
        {
            StartCoroutine("damagedelay");
        }

    }
    public IEnumerator damagedelay()
    {
        yield return new WaitForSeconds(0.001f);
        damaging = false;
        jump = false;
        smash = false;
    }

    public void respown()
    {
        foreach (GameObject enemy in defeatedEnemy)
        {
            enemy.transform.position = enemy.GetComponent<Collider2D>().transform.position;
            enemy.GetComponent<Renderer>().enabled = true;
            enemy.GetComponent<Collider2D>().enabled = true;
            print("ciao");

        }
    }
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (onGround)
        {
            StartCoroutine("damagedelay");

        }
    }

    
}
