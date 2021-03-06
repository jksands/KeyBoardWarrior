﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Regerence to the canvas for instantiation.
    public Canvas canvas;
    // Reference to the turn manager
    public TurnManager tm;
    // reference to the attack manager
    public AttackManager am;
    // Reference to the awesome Manager
    //public AwesomeManager aweMan;
    // String array of words
    private string[] words = { "attack", "item", "run"};
    // Holds the current word to type
    private string currentWord;
    // Holds the current char to be typed (stored in a string to make comparison easier in future)
    private string currentChar;
    // Current indec of the word.  Used to determine when to cycle
    private int currentIndex;
    // Length of the current word
    private int wordLength;
    // Index of the words array that you're currently on
    private int wordIndex;

    // Used for checking if the player is in a state to type (so if animations are playing then keystrokes won't register
    private bool canType = true;

    // This should hold the textboxes
    public GameObject[] empties;
    public Text healthBox;
    public Text overlay;

    // List to hold all text boxes
    private List<Text> textBoxes;
    // Holds all overlays
    private List<Text> overlays;
    // Possibly not needed anymore?
    // private List<Text> savedOverlays;
    // Holds active words
    private List<GameObject> active;
    // Holds valid indices
    private List<int> validIndices;
    // Holds indices for active words
    private List<int> indices;
    // Holds the index
    int index;
    // Index that tracks over all words
    private int globalIndex;
    // Holds current words
    private List<string> currentWords;
    //Dictionary that holds submenus.  Key = word that was jus ttyped!
    private Dictionary<string, string[]> subMenus;
    // Default menu
    private string currentMenu = "default";

    // used to check if user fucks up all three text boxes
    public float counter = 0;

    // Delay until player can type again (in seconds)
    public float delay = 1;

    // Used to keep track of menu depths (so you can go back properly)
    public Stack<string> menus;

    private bool counting = false;

    public int padding = 10;

    // Used to allow the player to target enemies
    public EnemyManager enemyManager;

    // Used to defend against attacks
    public List<GameObject> attacks;

    // Holds the actual words themselves
    public List<string> attackWords;

    // When you type an attack, the word progress you currently have will be reset.
    private bool attackRemoved;

    private string[] specialChars = { "!", "@", "#", "$", "%", "^", "&", "*", "z", "x", "q", "v", "/", ";", "w", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    private Dictionary<string, string> specialToKey;
    private int health = 10;
    private int maxHealth = 10;

    private bool startedTyping = false;
    public float speed = 0;

    // Script variable for how many words are active right now
    private int amount;

    public static bool playerTurn;

    public int indexer = 0;

    public List<GameObject> guage;

    public int special;
    private int bonus;
    private bool canBonus;
    private bool canSpecial = false;
    Text specialOverlay;


    // Start is called before the first frame update
    void Start()
    {
        canBonus = true;
        playerTurn = true;
        healthBox.color = Color.green;
        healthBox.text = health + "/" + maxHealth + " HP";
        // init stack
        menus = new Stack<string>();
        // Init dictionary
        subMenus = new Dictionary<string, string[]>();
        specialToKey = new Dictionary<string, string>();
        PopulateDictionary();
        subMenus.Add("default", new string[] { "attack", "special", "target" });
        subMenus.Add("attack", new string[] { "breakdance", "violince", "crash", "deathmetal", "bam-jo", "back" });
        subMenus.Add("special", new string[] {"vibrato-check", "ensnare-drum",  "keytar-solo", "back"});
        if (enemyManager.enemies.Count == 1)
        {

            subMenus.Add("target", new string[] {enemyManager.enemies[0].gameObject.name, 
            //enemyManager.enemies[1].gameObject.name, 
            // enemyManager.enemies[2].gameObject.name, 
            "back" });
        }
        else
        {
            subMenus.Add("target", new string[] {enemyManager.enemies[0].gameObject.name, 
            enemyManager.enemies[1].gameObject.name, 
            enemyManager.enemies[2].gameObject.name, 
            "back" });
        }
        // Single textboxes (not used anymore
        // textBox = empties[0].transform.GetChild(0).gameObject.GetComponent<Text>();
        // overlay = empties[0].transform.GetChild(1).gameObject.GetComponent<Text>();

        // init lists
        textBoxes = new List<Text>();
        overlays = new List<Text>();
        active = new List<GameObject>();
        validIndices = new List<int>();

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
        // canType = false;

        Vector3 p;
        for (int i = 0; i < guage.Count; i++)
        {
            // Populate the list with insantiated copies of themselves positioned correctly.
            guage[i] = Instantiate(guage[i], transform.position, Quaternion.identity, canvas.transform);
            p = transform.position;
            p.z -= 1;
            guage[i].transform.position = p;
            guage[i].transform.rotation = transform.rotation;
            guage[i].transform.localScale = transform.localScale;
            guage[i].SetActive(false);
        }

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
        // Init the menu system
        MakeActive(currentMenu);
        attackRemoved = false;
    }

    public  void ResetMenu()
    {

        subMenus.Remove("target");
        subMenus.Add("target", new string[] {enemyManager.viableEnemies[0].gameObject.name,
            enemyManager.viableEnemies[1].gameObject.name,
            enemyManager.viableEnemies[2].gameObject.name,
            "back" });

        Debug.Log(enemyManager.viableEnemies[1].gameObject.name);
        Debug.Log(enemyManager.viableEnemies[2].gameObject.name);
        Debug.Log("Reset Menu");
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
        // Next level Hacks
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (Map.activeIndex == 2)
            {
                // You win!
                SceneManager.LoadScene(7);
            }
            else
            {
                // Load the map again and increase progress
                Map.progress++;
                SceneManager.LoadScene(2);

            }
        }
        if (playerTurn)
        {
            if (counting)
            {
                counter += Time.deltaTime;
                if (counter > delay)
                {
                    canType = true;
                    counter = 0;
                    counting = false;
                    RemoveWrong();
                }
            }
            if (startedTyping && currentMenu == "attack")
            {
                speed += Time.deltaTime;
            }
            if (canType)
            {
                // Optimization.  Only loops if there is a key down!
                if (Input.anyKeyDown)
                {
                    // CheckAttacks();
                    if (!attackRemoved)
                    {
                        PlayerType();
                    }
                    else
                    {
                        // MakeActive(currentMenu);
                        ClearOverlays();
                        attackRemoved = false;
                    }

                }


            }
        }
        else
        {
            // Defen against active attacks if there are any.

            CheckAttacks();
        }

        // Debug.Log(active.Count);
        // // User fucked up typing all words
        // if (active.Count == 0)
        // {
        //     // Reset
        //     Debug.Log("Resetting");
        //     canType = false;
        // 
        //     MakeActive(currentMenu);
        // }
    }
    public void CheckAttacks()
    {
        char temp;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Cycling attackS");

            am.ChangeActiveAttack(am.activeIndex + 1);
        }
        if (am.activeAttack != null)
        {
            if (specialToKey.ContainsKey(am.activeText.text[indexer].ToString()))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // check if the user has pressed it
                    if (Input.GetKeyDown(specialToKey[am.activeText.text[indexer].ToString()]))
                    {
                        indexer = 0;
                        am.DestroyActive();

                    }
                }
            }
            else if (am.activeText.text[indexer] == ' ')
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    am.activeOverlay.text += " ";
                    indexer++;
                    if (indexer == am.activeText.text.Length)
                    {
                        indexer = 0;
                        am.DestroyActive();
                    }
                }
            }
            else if (Input.GetKeyDown((temp = am.activeText.text[indexer]).ToString()))
            {
                am.activeOverlay.text += temp;
                indexer++;
                if (indexer == am.activeText.text.Length)
                {
                    indexer = 0;
                    am.DestroyActive();
                }
            }
        }
        // Check attacks FIRST OLD CODE
        //for (int i = 0; i < attacks.Count; i++)
        //{
        //    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        //    {
        //        // If this is a special character, 
        //        if (specialToKey.ContainsKey(attackWords[i]))
        //        {
        //            // check if the user has pressed it
        //            if (Input.GetKeyDown(specialToKey[attackWords[i]]))
        //            {
        //                // If they have remove the attack
        //                attackRemoved = true;
        //                RemoveAttack(i);
        //                // Reset the overlays/ what the player has typed
        //                ClearOverlays();
        //                i--;
        //                break;

        //            }
        //        }
        //    }
        //    // if user typed one of the defensible chars (and shift is not held)
        //    else if (Input.GetKeyDown(attackWords[i]))
        //    {
        //        attackRemoved = true;
        //        RemoveAttack(i);
        //        i--;
        //        break;
        //    }
        //}
    }
    public void PlayerType()
    {
        for (int i = 0; i < validIndices.Count; i++)
        {
            index = validIndices[i];
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                // Do nothing?
            }
            // This checks if the user has typed the current char
            else if (Input.GetKeyDown((currentChar = currentWords[index][indices[index]].ToString())))
            {
                indices[index]++;
                startedTyping = true;
                // if (indices[index] == globalIndex)
                // {
                //     // Increment the character index
                //     indices[index]++;
                //     // Set the global index to THIS index
                //     globalIndex = indices[index];
                // }
                // // Otherwise, check the words and see if the char at globalindex is correct
                // else {
                //     // counter++;
                //     continue;
                // }

                // If we're not at the end of the word
                if (indices[index] != currentWords[index].Length)
                {
                    Debug.Log("Char guessed: " + currentChar);
                    // Get the next character
                    //overlays[i].text.Insert(indices[i], currentChar);
                    overlays[index].text += currentChar;

                    currentChar = currentWords[index][indices[index]].ToString();
                }
                else
                {
                    Debug.Log("Word Completed!");
                    startedTyping = false;
                    canType = false;
                    // aweMan.SpawnAwesome();
                    // Get the next word and reset information
                    if (currentWords[index] == "back")
                    {
                        MakeActive(currentMenu = menus.Pop());
                    }
                    // This is where we would access the "subMenu"
                    else if (subMenus.ContainsKey(currentWords[index]))
                    {
                        menus.Push(currentMenu);
                        currentMenu = currentWords[index];
                        MakeActive(currentMenu);
                    }
                    // We're currently in the target menu, so target an enemy
                    else if (currentMenu == "target")
                    {
                        // The selected enemy's index should match the index of the word in the array
                        enemyManager.TargetEnemy(enemyManager.viableEnemies[index]);
                        MakeActive(currentMenu = "default");
                    }
                    // We're in the attack menu, so attack the targeted enemy (or random enemy if no target)
                    else if (currentMenu == "attack")
                    {
                        int dmg = (int)(currentWords[index].Length - (speed * 2))*3;
                        if (dmg < 1) dmg = 1;
                        // Going simple, each attack does a different amount of damage. Not yet balanced for each word
                        enemyManager.DamageTarget(dmg); 
                        // Calculate special bonus
                        bonus = (int)(5.0f - speed);
                        if (canBonus && bonus > 0)
                        {
                            special += bonus;
                            if (special > 4) canSpecial = true;
                            ChangeGuage(special);
                        }
                        speed = 0;
                        MakeActive(currentMenu = "default");
                        tm.ChangeTurn();
                        
                    }
                    else if (currentMenu == "special") //Special menu, so activate some special effect
                    {
                        if (currentWords[index] == "vibrato-check")
                        {
                            for (int e = 0; e < enemyManager.enemies.Count; e++)
                            {
                                enemyManager.TargetEnemy(enemyManager.enemies[e]);
                                enemyManager.DamageTarget(15);
                            }
                        }
                        else if (currentWords[index] == "ensnare-drum")
                        {
                            am.attackSpeedMod = 1f;
                        }
                        else if (currentWords[index] == "keytar-solo")
                        {
                            enemyManager.DamageTarget(35); //Just a prototyping value
                        }

                        special -= 5;
                        if (special < 5) canSpecial = false;
                        ChangeGuage(special);
                        MakeActive(currentMenu = "default");
                        tm.ChangeTurn();
                        speed = 0;
                    }
                    else
                    {
                        MakeActive(currentMenu = "default");
                    }
                    canBonus = true;
                    // Break out of the for loop as the word was found
                    // And the list will be repopulated
                    break;
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
                // Increment the counter when a word is screwed up
                // counter++;
                validIndices.RemoveAt(i);
                i--;

                // If all words are screwed up, reset the entire menu
                if (validIndices.Count == 0)
                {
                    // counter = 0;
                    // Display all words red to user

                    canBonus = false;
                    canType = false;
                    ActivateWrong();
                    counting = true;
                    // break;
                }
            }

        }
    }
    public void ChangeGuage(int index)
    {
        if (index > guage.Count) index = guage.Count - 1;
        foreach (GameObject g in guage)
        {
            g.SetActive(false);
        }
        Debug.Log("Guage is now: " + index);
        guage[index].SetActive(true);
    }
    public void MakeActive(string key)
    {
        Debug.Log("!!!");
        globalIndex = 0;
        currentWords.Clear();
        // overlays.Clear();
        active.Clear();
        indices.Clear();
        validIndices.Clear();
        if (overlays.Count < 4) overlays.Insert(1, specialOverlay);
        string[] temp = subMenus[key];
        amount = temp.Length;
        List<string> toAdd = new List<string>(temp);
        toAdd.Remove("back");
        int wordsAdded = 0;
        if (currentMenu != "default" && currentMenu != "target")
        {


            // Add random words from toAdd (without back is always the last option)
            for (int i = 0; i < 3; i++) // 3 is hardcoded for how many words we want to generate
            {
                int x = Random.Range(0, toAdd.Count);
                currentWords.Add(toAdd[x]);
                overlays[i].text = "";
                textBoxes[i].text = toAdd[x];
                toAdd.RemoveAt(x);
            }
            overlays[3].text = "";
            textBoxes[3].text = "back";
            currentWords.Add("back");
        }

        // set all other boxes to inactive
        for (int i = 0; i < empties.Length; i++)
        {
            empties[i].SetActive(false);
            // indices[i] = 0;
            overlays[i].text = "";
            overlays[i].color = Color.yellow;
            textBoxes[i].color = Color.white;
            // overlays[i].color = Color.yellow;
        }
        if (amount > 4) amount = 4;
        // iterate through the options
        for (int i = 0; i < amount; i++)
        {
            if (currentMenu == "default" && i == 1 && !canSpecial)
            {
                Debug.Log("special: " + temp[i]);
                specialOverlay = overlays[i];
                overlays[i].color = Color.gray;
                textBoxes[i].text = temp[i];
                empties[i].SetActive(true);
                textBoxes[i].color = Color.gray;
                overlays.RemoveAt(i);
                Debug.Log("This shouldn't run twice");
                continue;
            }
            validIndices.Add(wordsAdded);
            empties[i].SetActive(true);
            indices.Add(0);
            active.Add(empties[i]);
            if (currentWords.Count == wordsAdded)
            {
                Debug.Log("^^^");
                Debug.Log("???????");
                currentWords.Add(temp[i]);
                overlays[wordsAdded].text = "";
                textBoxes[i].text = temp[i];
            }
            wordsAdded++;
        }
        canType = true;
    }

    // When user types incorrectly, turn everything red
    public void ActivateWrong()
    {

        for (int i = 0; i < active.Count; i++)
        {
            // If we can't special, the special option is grayed out.
            overlays[i].color = Color.red;
            overlays[i].text = currentWords[i];
        }
        //am.RemoveAttack(attacks[0]);
        // attacks.RemoveAt(0);
    }
    // Remove red when time is done
    public void RemoveWrong()
    {
        for (int i = 0; i < active.Count; i++)
        {
                overlays[i].color = Color.yellow;
                overlays[i].text = "";
        }
        // MakeActive(currentMenu);
        ClearOverlays();
    }

    public void ClearOverlays()
    {

        indices.Clear();
        validIndices.Clear();
        Debug.Log("???");
        int temp = 0;
        // iterate through the options
        for (int i = 0; i < amount; i++)
        {
            if (!canSpecial && currentMenu == "default" && i == 1) continue;
            validIndices.Add(temp);
            temp++;
            indices.Add(0);

            overlays[i].text = "";
        }
    }

    // Pinged by the attack manager.  Updates fields appropriately
    public void WasAttacked(GameObject attack)
    {
        Debug.Log("I was attacked by: " + attack.name);
        // overlays.Add(attack.transform.GetChild(1).gameObject.GetComponent<Text>().text);
        string temp = specialChars[Random.Range(0, specialChars.Length)];
        // string temp = "z";
        attackWords.Add(temp);
        attack.transform.GetChild(1).gameObject.GetComponent<Text>().text = temp;
        // overlays[overlays.Count - 1].text = "BAAAAAA";
        attacks.Add(attack);
    }

    public void RemoveAttack(int i)
    {
        am.RemoveAttack(attacks[i]);
        attacks.RemoveAt(i);
        attackWords.RemoveAt(i);
    }

    public void TakeDamage(int i)
    {
        Debug.Log("Hark!  I have taken the damage!");
        health -= i;
        if (health <= 3)
        {
            healthBox.color = Color.red;
        }
        healthBox.text = health + "/" + maxHealth + " HP";
        if (health <= 0)
        {
            SceneManager.LoadScene(6);
        }
    }

    // Populates the special dictionary
    public void PopulateDictionary()
    {
        
        for (int i = 0; i < 8; i++)
        {
            specialToKey.Add(specialChars[i], "" + (i + 1));
        }
    }

    
}
