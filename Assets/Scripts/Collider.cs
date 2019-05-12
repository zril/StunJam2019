using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collider : MonoBehaviour
{

    private GameObject maincamera;
    private AudioSource audiosource;
    private GameObject player;
    
    private int score;
    private int multiplier;
    private int combo;

    private int powerCounter;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        audiosource = maincamera.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        score = 0;
        multiplier = 1;
        combo = 0;
        powerCounter = 0;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "Letter")
        {
            if (collider.gameObject.tag == "Letter" && player.GetComponent<Player>().GetSmashing())
            {
                powerCounter++;
                UpdateUI();
                Destroy(collider.gameObject);

                var clip = maincamera.GetComponent<Main>().destroy;
                audiosource.PlayOneShot(clip);

                if (powerCounter == 5)
                {
                    var mire = GameObject.FindGameObjectWithTag("Mire");
                    mire.GetComponent<SpriteRenderer>().enabled = true;
                    mire.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    powerCounter = 0;

                    var clip2 = maincamera.GetComponent<Main>().power;
                    audiosource.PlayOneShot(clip2);
                }
            }
            else
            {
                combo = 0;
                multiplier = 1;
                powerCounter--;
                if (powerCounter < 0)
                {
                    powerCounter = 0;
                }

                UpdateUI();

                var clip = maincamera.GetComponent<Main>().hit;
                audiosource.PlayOneShot(clip);

                var mire = GameObject.FindGameObjectWithTag("Mire");
                mire.GetComponent<SpriteRenderer>().enabled = true;
                mire.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(MireTimer(0.05f));
            }
        }

        if (collider.gameObject.tag == "Bonus")
        {
            var value = collider.gameObject.GetComponent<Bonus>().scoreValue;

            score += value * multiplier;
            combo++;
            if (combo == 3)
            {
                combo = 0;
                multiplier++;
            }

            UpdateUI();

            Destroy(collider.gameObject);

            var clip = maincamera.GetComponent<Main>().bonus;
            audiosource.PlayOneShot(clip);
        }
    }


    private IEnumerator MireTimer(float mireTime)
    {
        yield return new WaitForSeconds(mireTime);
        var mire = GameObject.FindGameObjectWithTag("Mire");
        mire.GetComponent<SpriteRenderer>().enabled = false;
        mire.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateUI()
    {
        var scoreUI = GameObject.FindGameObjectWithTag("Score");
        scoreUI.GetComponent<Text>().text = score.ToString();

        var multiplierUI = GameObject.FindGameObjectWithTag("Multiplier");
        multiplierUI.GetComponent<Text>().text = multiplier.ToString() + "X";

        var powerbarUI = GameObject.FindGameObjectWithTag("PowerBar");
        powerbarUI.GetComponent<Image>().fillAmount = 0.20f * powerCounter;
    }
}
