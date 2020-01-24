using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        wordIndex = 0;
        currentIndex = 0;
        currentWord = words[wordIndex];
        currentChar = currentWord[0].ToString();
        wordLength = currentWord.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // This checks if the user has typed the current char
        // PROBLEM: How to detect if any other key was pressed?
        if (Input.GetKeyDown(currentChar))
        {
            currentIndex++;
            if (currentIndex != wordLength)
            {
                Debug.Log(currentChar);
                currentChar = currentWord[currentIndex].ToString();
            }
            else
            {
                Debug.Log("Word completed~");
                currentIndex = 0;
                wordIndex++;
                currentWord = words[wordIndex % words.Length];
                currentChar = currentWord[0].ToString();
                wordLength = currentWord.Length;
            }
        }
    }
}
