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

    // For future consideration
    private List<Text> textBoxes;
    private List<Text> overlays;
    private List<Text> savedOverlays;
    private List<GameObject> active;

    private List<string> currentWords;

    private Dictionary<string, string[]> subMenus;

    private string currentMenu = "default";
    

    // Start is called before the first frame update
    void Start()
    {
        subMenus = new Dictionary<string, string[]>();
        subMenus.Add("default", new string[] { "attack", "defend", "item" });
        subMenus.Add("attack", new string[] { "strongattack", "yeet", "yote" });
        textBox = empties[0].transform.GetChild(0).gameObject.GetComponent<Text>();
        overlay = empties[0].transform.GetChild(1).gameObject.GetComponent<Text>();

        textBoxes = new List<Text>();
        overlays = new List<Text>();
        active = new List<GameObject>();

        currentWords = new List<string>();

        savedOverlays = new List<Text>();
        // savedOverlays = new List<Text>(overlays);
        // For future consideration
        for (int i = 0; i < empties.Length; i++)
        {
            empties[i].SetActive(true);
            textBoxes.Add(empties[i].transform.GetChild(0).gameObject.GetComponent<Text>());
            overlays.Add(empties[i].transform.GetChild(1).gameObject.GetComponent<Text>());
            // savedOverlays.Add(empties[i].transform.GetChild(1).gameObject.GetComponent<Text>());
            active.Add(empties[i]);
            // list of words that are mapped to the textboxes
            //currentWords.Add(words[i]);
        }
        Debug.Log(overlays.Count);
        MakeActive(currentMenu);

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
            if (Input.GetKeyDown((currentChar = currentWords[i][currentIndex].ToString())))
            {
                // Increment the character index
                currentIndex++;
                // If we're not at the end of the word
                if (currentIndex != currentWords[i].Length)
                {
                    Debug.Log(currentChar);
                    // Get the next character
                    overlays[i].text += currentChar;
                    currentChar = currentWords[i][currentIndex].ToString();
                }
                else
                {
                    // Get the next word and reset information
                    Debug.Log("Word completed~");
                    overlays[i].text = "";
                    currentIndex = 0;

                    // This is where we would access the "subMenu"
                    if (subMenus.ContainsKey(currentWords[i]))
                    {
                        currentMenu = currentWords[i];
                        MakeActive(currentMenu);
                    }
                    else
                    {
                        MakeActive("default");
                    }
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
                // Reset back to the beginning of the menu
                Debug.Log("Word reset");
                overlays[i].text = "";
                // currentIndex = 0;
                // currentChar = currentWord[currentIndex].ToString();
                currentWords.RemoveAt(i);

                savedOverlays.Add(overlays[i]);
                overlays.RemoveAt(i);

                Debug.Log(savedOverlays.Count);
                active[i].SetActive(false);
                active.RemoveAt(i);
                i--;
            }
        }

        // User fucked up typing all words
        if (active.Count == 0)
        {
            // Reset
            MakeActive(currentMenu);
        }
    }

    public void MakeActive(string key)
    {
        currentWords.Clear();
        // overlays = savedOverlays;
        string[] temp = subMenus[key];
        // iterate through the options
        for (int i = 0; i < temp.Length; i++)
        {
            empties[i].SetActive(true);
            currentWords.Add(temp[i]);
            textBoxes[i].text = temp[i];
        }
    }
}
