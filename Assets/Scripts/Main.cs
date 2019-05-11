﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Dictionary<char, List<int>> MorseMap;

    public string text;
    public int bpm;

    private float time;

    private string currentText;
    private int currentIndex;

    private float timer;
    private float ticktimer;
    private int tickCounter;
    private int tickCounter3;

    private AudioSource audioSource;
    public AudioClip shortmorse;
    public AudioClip longmorse;
    public AudioClip letter;
    public AudioClip word;
    public AudioClip tick1;
    public AudioClip tick1_bis;
    public AudioClip tick2;
    public AudioClip tick2_bis;
    public AudioClip tick3;
    public AudioClip tick6;
    public AudioClip tick4;
    public AudioClip tick4_bis;
    public AudioClip tick41;
    public AudioClip tick41_bis;
    public AudioClip tick42;
    public AudioClip tick42_bis;
    public AudioClip tick43;
    public AudioClip tick43_bis;
    public AudioClip tick84;
    public AudioClip tick85;
    public AudioClip tick86;
    public AudioClip tick87;

    private float spawnOffsetX = 10;

    // Start is called before the first frame update
    void Start()
    {
        MorseMap = new Dictionary<char, List<int>>();
        MorseMap.Add('A', new List<int> { 0, 1 });
        MorseMap.Add('B', new List<int> { 1, 0, 0, 0 });
        MorseMap.Add('C', new List<int> { 1, 0, 1, 0 });
        MorseMap.Add('D', new List<int> { 1, 0, 0 });
        MorseMap.Add('E', new List<int> { 0 });
        MorseMap.Add('F', new List<int> { 0, 0, 1, 0 });
        MorseMap.Add('G', new List<int> { 1, 1, 0 });
        MorseMap.Add('H', new List<int> { 0, 0, 0, 0 });
        MorseMap.Add('I', new List<int> { 0, 0 });
        MorseMap.Add('J', new List<int> { 0, 1, 1, 1 });
        MorseMap.Add('K', new List<int> { 1, 0, 1 });
        MorseMap.Add('L', new List<int> { 0, 1, 0, 0 });
        MorseMap.Add('M', new List<int> { 1, 1 });
        MorseMap.Add('N', new List<int> { 1, 0 });
        MorseMap.Add('O', new List<int> { 1, 1, 1 });
        MorseMap.Add('P', new List<int> { 0, 1, 1, 0});
        MorseMap.Add('Q', new List<int> { 1, 1, 0, 1 });
        MorseMap.Add('R', new List<int> { 0, 1, 0 });
        MorseMap.Add('S', new List<int> { 0, 0, 0 });
        MorseMap.Add('T', new List<int> { 1 });
        MorseMap.Add('U', new List<int> { 0, 0, 1 });
        MorseMap.Add('V', new List<int> { 0, 0, 0, 1 });
        MorseMap.Add('W', new List<int> { 0, 1, 1 });
        MorseMap.Add('X', new List<int> { 1, 0, 0, 1 });
        MorseMap.Add('Y', new List<int> { 1, 0, 1, 1 });
        MorseMap.Add('Z', new List<int> { 1, 1, 0, 0 });

        time = 60f / (float)bpm;
        Time.timeScale = bpm / 240f;

        timer = time;
        ticktimer = time;
        tickCounter = 0;
        currentText = text;
        currentIndex = 0;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        ticktimer -= Time.deltaTime;

        if (ticktimer < 0)
        {
            ticktimer += time;
            audioSource.PlayOneShot(tick1);
            audioSource.PlayOneShot(tick1_bis);

            if (tickCounter == 0)
            {
                audioSource.PlayOneShot(tick2);
                audioSource.PlayOneShot(tick2_bis);
                audioSource.PlayOneShot(tick4);
                audioSource.PlayOneShot(tick4_bis);
            }

            if (tickCounter == 1)
            {
                audioSource.PlayOneShot(tick41);
                audioSource.PlayOneShot(tick41_bis);
            }

            if (tickCounter == 2)
            {
                audioSource.PlayOneShot(tick2);
                audioSource.PlayOneShot(tick2_bis);
                audioSource.PlayOneShot(tick42);
                audioSource.PlayOneShot(tick42_bis);
            }

            if (tickCounter == 3)
            {
                audioSource.PlayOneShot(tick43);
                audioSource.PlayOneShot(tick43_bis);
            }

            if (tickCounter == 4)
            {
                audioSource.PlayOneShot(tick2);
                audioSource.PlayOneShot(tick2_bis);
                audioSource.PlayOneShot(tick4);
                audioSource.PlayOneShot(tick4_bis);
                audioSource.PlayOneShot(tick84);
            }

            if (tickCounter == 5)
            {
                audioSource.PlayOneShot(tick41);
                audioSource.PlayOneShot(tick41_bis);
                audioSource.PlayOneShot(tick85);
            }

            if (tickCounter == 6)
            {
                audioSource.PlayOneShot(tick2);
                audioSource.PlayOneShot(tick2_bis);
                audioSource.PlayOneShot(tick42);
                audioSource.PlayOneShot(tick42_bis);
                audioSource.PlayOneShot(tick86);
            }

            if (tickCounter == 7)
            {
                audioSource.PlayOneShot(tick43);
                audioSource.PlayOneShot(tick43_bis);
                audioSource.PlayOneShot(tick87);
            }

            tickCounter++;
            if (tickCounter >= 8)
            {
                tickCounter = 0;
            }


            if (tickCounter3 == 0)
            {
                audioSource.PlayOneShot(tick3);
            }

            if (tickCounter3 == 3)
            {
                audioSource.PlayOneShot(tick3);
                audioSource.PlayOneShot(tick6);
            }

            tickCounter3++;
            if (tickCounter3 >= 6)
            {
                tickCounter3 = 0;
            }
        }

        if (timer < 0)
        {
            timer += time;
            if (currentText.Length > 0)
            {
                var currentLetter = currentText.ToUpper()[0];
                var currentMorse = MorseMap[currentLetter];
                if (currentIndex >= currentMorse.Count)
                {
                    currentIndex = 0;
                    currentText = currentText.Substring(1, currentText.Length - 1);

                    if (currentText.Length > 0)
                    {
                        if (currentText[0] == ' ')
                        {
                            InstantiateObject("Prefabs/Word", new Vector3(spawnOffsetX, 1, 0));
                            currentText = currentText.Substring(1, currentText.Length - 1);

                            timer += 6 * time;

                            audioSource.PlayOneShot(word);
                        }
                        else
                        {
                            timer += 2 * time;

                            InstantiateObject("Prefabs/Letter", new Vector3(spawnOffsetX, 1, 0));
                            audioSource.PlayOneShot(letter);
                        }
                    }
                }
                else
                {
                    Generate(currentText, currentIndex);
                    currentIndex++;
                }
            } else
            {
                InstantiateObject("Prefabs/Void", new Vector3(spawnOffsetX, 1, 0));

                timer += 6 * time;
            }
        }
    }

    private void Generate(string text, int index)
    {
        var currentLetter = text.ToUpper()[0];
        var currentMorse = MorseMap[currentLetter];
        var morse = currentMorse[index];

        var spikeHeightOffset = -0.4f;

        if (morse == 0)
        {
            timer += time;
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX, spikeHeightOffset, 0));
            audioSource.PlayOneShot(shortmorse);

            if (index < currentMorse.Count - 1)
            {
                InstantiateObject("Prefabs/Circle", new Vector3(spawnOffsetX + time * Global.Speed, 0, 0));
            }
        } else
        {
            timer += 3 * time;
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX, spikeHeightOffset, 0));
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX + time * Global.Speed, spikeHeightOffset, 0));
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX + 2 * time * Global.Speed, spikeHeightOffset, 0));
            audioSource.PlayOneShot(longmorse);

            if (index < currentMorse.Count - 1)
            {
                InstantiateObject("Prefabs/Circle", new Vector3(spawnOffsetX + 3 * time * Global.Speed, 0, 0));
            }
        }
    }

    private void InstantiateObject(string prefab, Vector3 position)
    {
        var obj = Instantiate(Resources.Load(prefab), position, Quaternion.identity);
        Destroy(obj, 8f);
    }
}
