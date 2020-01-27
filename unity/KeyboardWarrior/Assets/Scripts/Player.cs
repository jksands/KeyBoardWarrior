﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // String array of words
    string[] words = { "attack", "defend", "item", "run"};
    // Holds the current word to type
    string currentWord;
    // Holds the current char to be typed (stored in a string to make comparison easier in future)
    string currentChar;
    // Current indec of the word.  Used to determine when to cycle
    int currentIndex;
    // Length of the current word
    int wordLength;
    // Index of the words array that you're currently on
    int wordIndex;

    // Used for checking if the player is in a state to type (so if animations are playing then keystrokes won't register
    bool canType = true;

    // This should hold the textboxes
    public GameObject[] empties;
    public Text textBox;
    public Text overlay;

    // For future consideration
    private List<Text> textBoxes;
    private List<Text> overlays;
    private List<GameObject> active;

    private List<string> currentWords;
    

    // Start is called before the first frame update
    void Start()
    {
        textBox = empties[0].transform.GetChild(0).gameObject.GetComponent<Text>();
        overlay = empties[0].transform.GetChild(1).gameObject.GetComponent<Text>();

        textBoxes = new List<Text>();
        overlays = new List<Text>();
        active = new List<GameObject>();

        currentWords = new List<string>();

        // For future consideration
       for(int i = 0; i < empties.Length;  i++)
       {
           empties[i].SetActive(true);
           textBoxes.Add(empties[i].transform.GetChild(0).gameObject.GetComponent<Text>());
           overlays.Add(empties[i].transform.GetChild(1).gameObject.GetComponent<Text>());
           active.Add(empties[i]);
           // list of words that are mapped to the textboxes
            currentWords.Add(words[i]);
       }

        wordIndex = 0;
        currentIndex = 0;
        currentWord = words[wordIndex];
        currentChar = currentWord[0].ToString();
        wordLength = currentWord.Length;
        textBox.text = currentWord;
        overlay.text = "";
        overlay.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        // Quit this shit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            // SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        }
        for (int i = 0; i < active.Count; i++)
        {
            // This checks if the user has typed the current char
            if (Input.GetKeyDown(currentWords[i][currentIndex].ToString()))
            {
                // Increment the character index
                currentIndex++;
                // If we're not at the end of the word
                if (currentIndex != words[i].Length)
                {
                    Debug.Log(currentChar);
                    overlays[i].text += currentChar;
                    // Get the next character
                    currentChar = currentWords[i][currentIndex].ToString();
                }
                else
                {
                    // Get the next word and reset information
                    Debug.Log("Word completed~");
                    overlay.text = "";
                    currentIndex = 0;
                    wordIndex++;
                    currentWord = words[wordIndex % words.Length];
                    currentChar = currentWord[0].ToString();
                    wordLength = currentWord.Length;
                    // UPdate the word to the screen
                    textBox.text = currentWord;
                }
            }
            // User fucked up
            else if (Input.anyKeyDown)
            {
                // Reset back to the beginning of the word
                Debug.Log("Word reset");
                overlays[i].text = "";
                // currentIndex = 0;
                // currentChar = currentWord[currentIndex].ToString();
                active[i].SetActive(false);
                active.RemoveAt(i);
                i--;
            }
        }
    }
}
