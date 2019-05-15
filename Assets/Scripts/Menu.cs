using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    Main main;

    private float nomireTimer;
    private bool lockInput;

    private bool initTitle;

    private int levelIndex;

    private string[,] levels;


    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        nomireTimer = 0;
        main = gameObject.GetComponent<Main>();

        lockInput = false;

        if (Global.levelText == "select")
        {
            Debug.Log("select");
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            initTitle = true;
        } else
        {
            for (int i = 0; i < 4; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            initTitle = false;
        }

        levelIndex = 0;

        levels = new string[,] {
            { "Controles", "A pour Sauter. X pour Frapper." },
            { "Newbie", "SOS. SOS. SOS. SOS. SOS. SOS. SOS. SOS. SOS." },
			{ "Niveau 2", "a aa e ee i ii o oo u uu y yy" },
			{ "Niveau 3", "tati titati titotiteta titotetitatu tu tu" },
			{ "Sonic", "It's me, Mario" },
			{ "Pikachu", "PIEEEEEEEEERRE" },
			{ "Morse", "Le code Morse international, est un code inventé en 1832 pour la télégraphie et permettant de transmettre un texte à l’aide de séries d’impulsions courtes et longues, qu’elles soient produites par des signes, une lumière, un son ou un geste." },
			{ "Fibonacci", "1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, etc" },
			{ "Braddock", "Je mets les pieds où je veux Little John, et c'est souvent dans la gueule." },
			{ "Latin", "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
			{ "Camarade", "Ami, entends tu le vol noir des corbeaux sur nos plaines" },
			{ "Coiffeur", "Belle journée. On a du mal à croire qu'on est en mai." },
            { "Tutoriel", "Aidez la mire de calibrage à convertir le texte en code MORSE déstiné aux millions d'usagers désireux de faire savoir à leurs proches les nouvelles essentielles de leurs vies, sans les inconvénients de temps et d'argent qu'impliquent l'emploi d'un messager, et parce que ça rapporte des POINTS. SAUTEZ par dessus les arcs électrostatiques résiduels et détruisez les écrans plats. Ces derniers renferment un pouvoir multimédiatique sans commune mesure, augmentant ainsi votre multiplicateur de SCORE de façon significative. Si le multiplicateur est rempli, vous passez en mode SUPER PLAY, vous devrez alors mettre en oeuvre tout ce que ce TUTORIEL vous aura appris sur le MORSE pour braver la cécité gratifiante. La fonctionnalité de HIGH SCORE ainsi que le mode VR seront disponibles dans de prochains DLC, en attendant n'hésitez pas à écrire votre HIGH SCORE sur un POST IT et à le COLLER sur l'écran." },
            { "2 joueurs", "Utilisez le clavier pour improviser le texte" } };

        

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        nomireTimer += Time.deltaTime;

        var nomireTime = 30f;
        if (nomireTimer > nomireTime)
        {
            main.SetCurrentText("nomire");
            nomireTimer -= nomireTime;
        }

        if (initTitle)
        {
            if (!lockInput)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    levelIndex++;
                    if (levelIndex >= levels.GetLength(0))
                    {
                        levelIndex = levels.GetLength(0) - 1;
                    }
                    lockInput = true;
                    UpdateUI();
                }

                if (Input.GetAxis("Vertical") > 0)
                {
                    levelIndex--;
                    if (levelIndex < 0)
                    {
                        levelIndex = 0;
                    }
                    lockInput = true;
                    UpdateUI();
                }
            } else
            {
                if (Input.GetAxis("Vertical") == 0)
                {
                    lockInput = false;
                }
            }

            if (Input.GetButtonDown("Button2"))
            {
                if (levelIndex < levels.GetLength(0) - 1)
                {
                    Global.levelText = levels[levelIndex, 1];
                } else
                {
                    Global.levelText = "";
                }

                SceneManager.LoadScene(1);
             }
        }

        if (!initTitle && Input.anyKeyDown)
        {
            for (int i = 0; i < 4; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(true);
            }
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            initTitle = true;
        }

        //reset
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Global.levelText = "title";
            SceneManager.LoadScene(0);
        }

    }

    private void UpdateUI()
    {
        if (levelIndex > 0)
        {
            SetChoix(0, levels[levelIndex - 1, 0]);
        } else
        {
            SetChoix(0, "");
        }

        SetChoix(1, levels[levelIndex, 0]);
        var preview = levels[levelIndex, 1];
        var maxlength = 35;
        if (preview.Length > maxlength)
        {
            SetPreview(preview.Substring(0, maxlength) + "…");
        } else
        {
            SetPreview(preview);
        }

        if (levelIndex < levels.GetLength(0) - 1)
        {
            SetChoix(2, levels[levelIndex + 1, 0]);
        }
        else
        {
            SetChoix(2, "");
        }

    }

    private void SetChoix(int choix, string text)
    {
        canvas.transform.GetChild(choix).GetChild(0).GetComponent<Text>().text = text;
    }

    private void SetPreview(string text)
    {
        canvas.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = text;
    }
}
