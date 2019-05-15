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
        Global.levelText = "title";

        nomireTimer = 0;
        main = gameObject.GetComponent<Main>();

        lockInput = false;
        initTitle = false;

        levelIndex = 0;

        levels = new string[,] {
            { "test", "tessssssssssssssssssssssssssssssssssssssssssssssssssst" },
            { "test2", "ghzthzth zzetg" },
            { "test3", "gthr ztzt" },
            { "Tutorial", "blablabla" },
            { "2 players", "Use your keyboard" } };

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        for (int i = 0; i <4; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }

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
