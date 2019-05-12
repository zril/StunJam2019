using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    Dictionary<char, List<int>> MorseMap;

    public string text;
    public int bpm;

    private float time;

    private string currentText;
    private string currentTextUI;
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
    
    public AudioClip hit;

    private float spawnOffsetX = 14.13f;
    private float soundDelay = 4f;

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
        MorseMap.Add('\'', new List<int> { 0, 1, 1, 1, 1, 0 });

        time = 60f / (float)bpm;
        Time.timeScale = bpm / 240f;

        timer = time;
        ticktimer = time;
        tickCounter = 0;
        currentText = text;
        currentTextUI = text;
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
                    StartCoroutine(RemoveLetterUI(soundDelay));

                    if (currentText.Length > 0)
                    {
                        if (currentText[0] == ' ' || currentText[0] == ',' || currentText[0] == '.')
                        {
                            //InstantiateObject("Prefabs/Word", new Vector3(spawnOffsetX, 1, 0));
                            InstantiateObject("Prefabs/Circle2", new Vector3(spawnOffsetX + 1 * time * Global.Speed, 1.5f, 0));
                            InstantiateObject("Prefabs/Circle", new Vector3(spawnOffsetX + 2 * time * Global.Speed, 1.5f, 0));
                            InstantiateObject("Prefabs/Circle", new Vector3(spawnOffsetX + 3 * time * Global.Speed, 1.5f, 0));
                            InstantiateObject("Prefabs/Circle2", new Vector3(spawnOffsetX + 4 * time * Global.Speed, 1.5f, 0));
                            currentText = currentText.Substring(1, currentText.Length - 1);
                            StartCoroutine(RemoveLetterUI(soundDelay));

                            timer += 6 * time;
                            
                            StartCoroutine(PlaySoundWithDelay(word, soundDelay));

                            while (currentText[0] == ' ')
                            {
                                currentText = currentText.Substring(1, currentText.Length - 1);
                                StartCoroutine(RemoveLetterUI(soundDelay));
                            }
                        }
                        else
                        {
                            timer += 2 * time;

                            InstantiateObject("Prefabs/Letter", new Vector3(spawnOffsetX, 0.5f, 0));
                            StartCoroutine(PlaySoundWithDelay(letter, soundDelay));
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
                //InstantiateObject("Prefabs/Void", new Vector3(spawnOffsetX, 1, 0));

                timer += 6 * time;
            }
        }


        //text input

        var inputstring = Input.inputString;
        var inputchars = inputstring.ToCharArray();
        foreach (char c in inputchars)
        {
            var b = false;
            var ponct = false;
            if (c >= 'a' && c <= 'z')
            {
                b = true;
            }
            if (c >= 'A' && c <= 'Z')
            {
                b = true;
            }
            if (c == ' ' || c == ',' || c == '.' || c == '\'')
            {
                b = true;
                ponct = true;
            }

            if (b && !(ponct && currentText.Length == 0))
            {
                currentText = currentText + c;
                currentTextUI = currentTextUI + c;
            }
        }

        //UI
        var textui = GameObject.FindGameObjectWithTag("CurrentText");
        var text1 = "";
        var text2 = "";
        if (currentTextUI.Length > 1)
        {
            text1 = currentTextUI.Substring(0, 1);
            text2 = currentTextUI.Substring(1, currentTextUI.Length - 1);
        }
        else if (currentTextUI.Length == 1)
        {
            text1 = currentTextUI;
        }

        textui.GetComponent<Text>().text = "<color=#ff0000ff>" + text1 + "</color>" + text2;
    }

    private void Generate(string text, int index)
    {
        var majuscule = Char.IsUpper(text[0]);
        var currentLetter = text.ToUpper()[0];
        var currentMorse = MorseMap[currentLetter];
        var morse = currentMorse[index];

        //var spikeHeightOffset = -0.4f;
        var spikeHeightOffset = 0.1f;

        if (morse == 0)
        {
            timer += time;
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX, spikeHeightOffset, 0));
            StartCoroutine(PlaySoundWithDelay(shortmorse, soundDelay));

            if (index < currentMorse.Count - 1)
            {
                var prefab = "Circle";
                if (majuscule)
                {
                    prefab = "Circle2";
                }
                InstantiateObject("Prefabs/" + prefab, new Vector3(spawnOffsetX + time * Global.Speed, 0.3f, 0));
            }
        } else
        {
            timer += 3 * time;
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX, spikeHeightOffset, 0));
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX + time * Global.Speed, spikeHeightOffset, 0));
            InstantiateObject("Prefabs/Spike", new Vector3(spawnOffsetX + 2 * time * Global.Speed, spikeHeightOffset, 0));
            StartCoroutine(PlaySoundWithDelay(longmorse, soundDelay));

            if (index < currentMorse.Count - 1)
            {
                var prefab = "Circle";
                if (majuscule)
                {
                    prefab = "Circle2";
                }
                InstantiateObject("Prefabs/" + prefab, new Vector3(spawnOffsetX + 3 * time * Global.Speed, 0.3f, 0));
            }
        }
    }

    private void InstantiateObject(string prefab, Vector3 position)
    {
        var obj = Instantiate(Resources.Load(prefab), position, Quaternion.identity);
        Destroy(obj, 8f);
    }

    private IEnumerator PlaySoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

    private IEnumerator RemoveLetterUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentTextUI = currentTextUI.Substring(1, currentTextUI.Length - 1);
    }
}
