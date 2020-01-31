using System.Collections;
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

    // List to hold all text boxes
    private List<Text> textBoxes;
    // Holds all overlays
    private List<Text> overlays;
    // Possibly not needed anymore?
    // private List<Text> savedOverlays;
    // Holds active words
    private List<GameObject> active;
    // Holds indices for active words
    private List<int> indices;
    // Holds current words
    private List<string> currentWords;
    //Dictionary that holds submenus.  Key = word that was jus ttyped!
    private Dictionary<string, string[]> subMenus;
    // Default menu
    private string currentMenu = "default";

    // used to check if user fucks up all three text boxes
    private int counter = 0;

    // Used to keep track of menu depths (so you can go back properly)
    public Stack<string> menus;
    

    // Start is called before the first frame update
    void Start()
    {

        // init stack
        menus = new Stack<string>();
        // menus.Push("default");      // Default menu is top level
        // Init dictionary
        subMenus = new Dictionary<string, string[]>();
        subMenus.Add("default", new string[] { "attack", "defend", "item" });
        subMenus.Add("attack", new string[] { "strongattack", "yeet", "yote", "back" });
        // Single textboxes (not used anymore
        // textBox = empties[0].transform.GetChild(0).gameObject.GetComponent<Text>();
        // overlay = empties[0].transform.GetChild(1).gameObject.GetComponent<Text>();

        // init lists
        textBoxes = new List<Text>();
        overlays = new List<Text>();
        active = new List<GameObject>();

        currentWords = new List<string>();
        //Word indices
        indices = new List<int>();

        // Populate empties with textboxes and set empties to active
        for (int i = 0; i < empties.Length; i++)
        {
            // empties[i].SetActive(true);
            textBoxes.Add(empties[i].transform.GetChild(0).gameObject.GetComponent<Text>());
            overlays.Add(empties[i].transform.GetChild(1).gameObject.GetComponent<Text>());
            // active.Add(empties[i]);
            indices.Add(0);
        }
        // Init the menu system
        MakeActive(currentMenu);



        // Not needed anymore?
        // wordIndex = 0;
        // currentIndex = 0;
        // currentWord = words[wordIndex];
        // currentChar = currentWord[0].ToString();
        // wordLength = currentWord.Length;
        // textBox.text = currentWord;
        // overlay.text = "";
        // overlay.color = Color.yellow;

        for (int i = 0; i < overlays.Count; i++)
        {
            overlays[i].color = Color.yellow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter = 0;
        // Quit this shit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            // SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        }
        Debug.Log("active Count: " + active.Count);
        for (int i = 0; i < active.Count; i++)
        {
            // This checks if the user has typed the current char
            if (Input.GetKeyDown((currentChar = currentWords[i][indices[i]].ToString())))
            {
                // Increment the character index
                indices[i]++;
                // If we're not at the end of the word
                if (indices[i] != currentWords[i].Length)
                {
                    Debug.Log(currentChar);
                    // Get the next character
                    overlays[i].text += currentChar;
                    currentChar = currentWords[i][indices[i]].ToString();
                }
                else
                {
                    // Get the next word and reset information
                    if (currentWords[i] == "back")
                    {
                        MakeActive(currentMenu = menus.Pop());
                    }
                    // This is where we would access the "subMenu"
                    else if (subMenus.ContainsKey(currentWords[i]))
                    {
                        menus.Push(currentMenu);
                        currentMenu = currentWords[i];
                        MakeActive(currentMenu);
                    }
                    else
                    {
                        MakeActive(currentMenu = "default");
                    }

                    // Not needed anymore 
                    // wordIndex++;
                    // currentWord = words[wordIndex % words.Length];
                    // currentChar = currentWord[0].ToString();
                    // wordLength = currentWord.Length;
                    // // UPdate the word to the screen
                    // textBox.text = currentWord;
                }
            }
            // User fucked up
            else if (Input.anyKeyDown)
            {
                // Increemtn the counter when a word is screwed up
                counter++;

                // If all words are screwed up, reset the entire menu
                if (counter == indices.Count)
                {
                    counter = 0;
                    MakeActive(currentMenu);
                }
            }

        }

        // User fucked up typing all words
        if (active.Count == 0)
        {
            // Reset
            Debug.Log("Resetting");
            MakeActive(currentMenu);
        }
    }

    public void MakeActive(string key)
    {
        currentWords.Clear();
        // overlays.Clear();
        active.Clear();
        string[] temp = subMenus[key];
        // Debug.Log("length: " + temp.Length);

        // iterate through the options
        for (int i = 0; i < temp.Length; i++)
        {
            empties[i].SetActive(true);
            indices[i] = 0;
            overlays[i].text = "";
            active.Add(empties[i]);
            // Debug.Log("Empties? SHould be 3: " + 3);
            // overlays.Add(savedOverlays[i]);
            // Debug.Log("Overlays: " + overlays.Count);
            currentWords.Add(temp[i]);
            textBoxes[i].text = temp[i];
        }

        // set all other boxes to inactive
        for (int i = temp.Length; i < empties.Length; i++)
        {
            empties[i].SetActive(false);
            indices[i] = 0;
            overlays[i].text = "";
        }
    }
}
