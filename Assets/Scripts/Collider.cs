using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{

    private GameObject maincamera;
    private AudioSource audiosource;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        audiosource = maincamera.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "Letter")
        {
            if (collider.gameObject.tag == "Letter" && player.GetComponent<Player>().GetSmashing())
            {
                Destroy(collider.gameObject);
            }
            else
            {
                var clip = maincamera.GetComponent<Main>().hit;
                audiosource.PlayOneShot(clip);

                var mire = GameObject.FindGameObjectWithTag("Mire");
                mire.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(MireTimer(0.1f));
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
