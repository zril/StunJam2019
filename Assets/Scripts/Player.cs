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
    private float rollTimer;
    private float smashTime = 0.25f;
    //private float rollTime = 0.4f;
    private float rollTime = 0.0f;
    private bool smashing = false;
    private bool rolling = false;

    private GameObject maincamera;
    private AudioSource audiosource;
    private Animator animator;
    private GameObject collider;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onGround = true;

        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        audiosource = maincamera.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        collider = GameObject.FindGameObjectWithTag("Collider");
    }

    // Update is called once per frame
    void Update()
    {
        //jump
        if (Input.GetButtonDown("Button1") && onGround && !rolling && !smashing)
        {
            onGround = false;
            jumpTimer = 0;
            rb.AddForce(transform.up * jumpForceInit, ForceMode2D.Impulse);

            animator.SetBool("Jump", true);
        }

        if (Input.GetButton("Button1") && onGround && !rolling && !smashing)
        {
            onGround = false;
            jumpTimer = 0;
            rb.AddForce(transform.up * jumpForceInit, ForceMode2D.Impulse);

            animator.SetBool("Jump", true);
            animator.SetBool("Rejump", true);
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
        if (Input.GetButtonDown("Button2") && !smashing)
        {
            smashTimer = smashTime;
            smashing = true;
            animator.SetBool("Smash", true);

            collider.transform.position = collider.transform.position + new Vector3(0.3f, 0, 0);
        }
        
        if (smashing)
        {
            smashTimer -= Time.fixedDeltaTime;
            if (smashTimer <= 0)
            {
                smashing = false;
                animator.SetBool("Smash", false);
                rolling = true;
                animator.SetBool("Roll", true);
                rollTimer = rollTime;
                collider.transform.position = collider.transform.position - new Vector3(0.3f, 0, 0);
            }
        }

        if (rolling)
        {
            rollTimer -= Time.fixedDeltaTime;

            if (rollTimer < 0)
            {
                rolling = false;
                animator.SetBool("Roll", false);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("Jump", false);
            animator.SetBool("Rejump", false);
        }
    }

    public bool GetSmashing()
    {
        return smashing;
    }

    
}
