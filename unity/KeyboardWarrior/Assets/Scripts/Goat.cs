using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goat : Enemy
{

    public Canvas canvas;
    public GameObject textBox;
    private GameObject visibleBox;
    private Vector3 boxPos;
    public Text wordText;
    public Text overlay;

    public List<string> goatWords;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        // Set the enemy's max health to it's given health
        health = maxHealth;

        // Set the enemy's info to display
        nameBox.text = gameObject.name;
        healthBox.text = health + "/" + maxHealth + " HP";
        healthBox.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
