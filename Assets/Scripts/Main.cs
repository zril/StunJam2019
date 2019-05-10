using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Dictionary<char, List<int>> MorseMap;

    private string text = "ab cde";

    private string currentText;
    private int currentIndex;

    private float timer;
    private float time = 0.25f;

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

        

        timer = time;
        currentText = text;
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer += time;
            if (currentText.Length > 0)
            {
                if (currentText[0] == ' ')
                {
                    Instantiate(Resources.Load("Prefabs/Word"), new Vector3(0, 1, 0), Quaternion.identity);
                    currentText = currentText.Substring(1, currentText.Length - 1);

                    timer += 7 * time;
                } else
                {
                    var currentLetter = currentText.ToUpper()[0];
                    var currentMorse = MorseMap[currentLetter];
                    if (currentIndex >= currentMorse.Count)
                    {
                        currentIndex = 0;
                        currentText = currentText.Substring(1, currentText.Length - 1);
                        timer += 3 * time;
                        Instantiate(Resources.Load("Prefabs/Letter"), new Vector3(0, 1, 0), Quaternion.identity);
                    }
                    else
                    {
                        Generate(currentText, currentIndex);
                        currentIndex++;
                    }
                }
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
        } else
        {
            timer += 3 * time;
        }
    }
}
