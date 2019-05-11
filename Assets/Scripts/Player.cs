using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool onGround;
    private float jumpTimer;

    private float jumpTime = 1f;
    private float jumpForceInit = 5f;
    private float jumpForce = 0.28f;

    private float smashTimer;
    private float smashTime = 0.25f;
    private bool smashing = false;

    private GameObject maincamera;
    private AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onGround = true;

        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        audiosource = maincamera.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //jump
        if (Input.GetButtonDown("Button1") && onGround)
        {
            onGround = false;
            jumpTimer = 0;
            rb.AddForce(transform.up * jumpForceInit, ForceMode2D.Impulse);
        }

        if (Input.GetButton("Button1") && onGround)
        {
            onGround = false;
            jumpTimer = 0;
            rb.AddForce(transform.up * jumpForceInit, ForceMode2D.Impulse);
        }

        if (Input.GetButton("Button1") && jumpTimer < jumpTime && !onGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer += Time.fixedDeltaTime;
        }

        if (Input.GetButtonUp("Button1") && !onGround)
        {
            jumpTimer = jumpTime;
        }

        //smash
        if (Input.GetButtonDown("Button2"))
        {
            smashTimer = smashTime;
            smashing = true;

            transform.position = transform.position + new Vector3(0.25f, 0, 0);
        }

        if (smashing)
        {
            smashTimer -= Time.fixedDeltaTime;
            if (smashTimer < 0)
            {
                smashing = false;
                transform.position = transform.position + new Vector3(-0.25f, 0, 0);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "Letter")
        {
            if (collider.gameObject.tag == "Letter" && smashing)
            {
                Destroy(collider.gameObject);
            } else
            {
                var mire = GameObject.FindGameObjectWithTag("Mire");
                mire.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(MireTimer(0.1f));

                var clip = maincamera.GetComponent<Main>().hit;
                audiosource.PlayOneShot(clip);
            }
        }

        if (collider.gameObject.tag == "Bonus")
        {
            Destroy(collider.gameObject);
        }
    }


    private IEnumerator MireTimer(float mireTime)
    {
        yield return new WaitForSeconds(mireTime);
        var mire = GameObject.FindGameObjectWithTag("Mire");
        mire.GetComponent<SpriteRenderer>().enabled = false;
    }
}
