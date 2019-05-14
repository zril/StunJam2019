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

    private int levelIndex;

    private string[,] levels;

    // Start is called before the first frame update
    void Start()
    {
        nomireTimer = 0;
        main = gameObject.GetComponent<Main>();

        lockInput = false;

        levelIndex = 0;

        levels = new string[,] {
            { "test", "gzetg" },
            { "test2", "ghzthzth zzetg" },
            { "test3", "gthr ztzt" },
            { "Tutorial", "blablabla" },
            { "2 players", "Use your keyboard" } };

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        nomireTimer += Time.deltaTime;

        var nomireTime = 35f;
        if (nomireTimer > nomireTime)
        {
            main.SetCurrentText("nomire");
            nomireTimer -= nomireTime;
        }

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
        SetPreview(levels[levelIndex, 1]);

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
        var canvas = GameObject.FindGameObjectWithTag("Canvas");

        canvas.transform.GetChild(choix).GetChild(0).GetComponent<Text>().text = text;
    }

    private void SetPreview(string text)
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");

        canvas.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = text;
    }
}
