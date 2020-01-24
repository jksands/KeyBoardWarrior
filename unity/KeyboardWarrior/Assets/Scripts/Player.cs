using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text textBox;
    

    // Start is called before the first frame update
    void Start()
    {
        wordIndex = 0;
        currentIndex = 0;
        currentWord = words[wordIndex];
        currentChar = currentWord[0].ToString();
        wordLength = currentWord.Length;
        textBox.text = currentWord;
    }

    // Update is called once per frame
    void Update()
    {
        // This checks if the user has typed the current char
        if (Input.GetKeyDown(currentChar))
        {
            // Increment the character index
            currentIndex++;
            // If we're not at the end of the word
            if (currentIndex != wordLength)
            {
                Debug.Log(currentChar);
                // Get the next character
                currentChar = currentWord[currentIndex].ToString();
            }
            else
            {
                // Get the next word and reset information
                Debug.Log("Word completed~");
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
            currentIndex = 0;
            currentChar = currentWord[currentIndex].ToString();
        }
    }
}
