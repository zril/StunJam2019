using System.Collections;
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
    public AudioClip tick3_bis;
    public AudioClip tick4;
    public AudioClip tick4_bis;
    public AudioClip tick41;
    public AudioClip tick41_bis;
    public AudioClip tick42;
    public AudioClip tick42_bis;
    public AudioClip tick43;
    public AudioClip tick43_bis;

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

            tickCounter++;
            if (tickCounter >= 4)
            {
                tickCounter = 0;
            }

            if (tickCounter3 == 0)
            {
                audioSource.PlayOneShot(tick3);
                audioSource.PlayOneShot(tick3_bis);
            }

            tickCounter3++;
            if (tickCounter3 >= 3)
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
                            Instantiate(Resources.Load("Prefabs/Word"), new Vector3(0, 1, 0), Quaternion.identity);
                            currentText = currentText.Substring(1, currentText.Length - 1);

                            timer += 6 * time;

                            audioSource.PlayOneShot(shortmorse);
                        }
                        else
                        {
                            timer += 2 * time;
                            Instantiate(Resources.Load("Prefabs/Letter"), new Vector3(0, 1, 0), Quaternion.identity);

                            audioSource.PlayOneShot(shortmorse);
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
                Instantiate(Resources.Load("Prefabs/Void"), new Vector3(0, 1, 0), Quaternion.identity);

                timer += 6 * time;
            }
        }
    }

    private void Generate(string text, int index)
    {
        var currentLetter = text.ToUpper()[0];
        var currentMorse = MorseMap[currentLetter];
        var morse = currentMorse[index];

        Instantiate(Resources.Load("Prefabs/Square"), new Vector3(0, morse * 2, 0), Quaternion.identity);
        if (morse == 0)
        {
            timer += time;
            audioSource.PlayOneShot(shortmorse);
        } else
        {
            timer += 3 * time;
            audioSource.PlayOneShot(longmorse);
        }
    }
}
