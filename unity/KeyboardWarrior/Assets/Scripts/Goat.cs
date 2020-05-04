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

    public List<string> goatWords;// = { "!", "@", "#", "$", "%", "^", "&", "*", "z", "x", "q", "v", "/", ";", "w", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    // Start is called before the first frame update
    new void Start()
    {
        maxHealth = 100;
        // Set the enemy's max health to it's given health
        health = maxHealth;

        // Set the enemy's info to display
        nameBox.text = gameObject.name;
        healthBox.text = health + "/" + maxHealth + " HP";
        healthBox.color = Color.green;

        goatWords.Add("!"); 
        goatWords.Add("@"); 
        goatWords.Add("#"); 
        goatWords.Add("$"); 
        goatWords.Add("%"); 
        goatWords.Add("^"); 
        goatWords.Add("&"); 
        goatWords.Add("*"); 
        goatWords.Add("z"); 
        goatWords.Add("x"); 
        goatWords.Add("q"); 
        goatWords.Add("v"); 
        goatWords.Add("/"); 
        goatWords.Add(";"); 
        goatWords.Add("w"); 
        goatWords.Add("1"); 
        goatWords.Add("2"); 
        goatWords.Add("3"); 
        goatWords.Add("4"); 
        goatWords.Add("5"); 
        goatWords.Add("6"); 
        goatWords.Add("7");
        goatWords.Add("8"); 
        goatWords.Add("9");
        Debug.Log(goatWords.Count);
        Debug.Log(goatWords[1]);
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        Debug.Log("Goat Attacking");
        // sheepAttacking = true;
        AttackManager.enemyAttacking = true;
        Vector3 position = transform.position;
        position.y -= 1;
        position.x += 1;
        visibleBox = Instantiate(textBox, position, Quaternion.identity, canvas.transform);
        Text[] temp = visibleBox.GetComponentsInChildren<Text>();
        wordText = temp[0];
        wordText.text = goatWords[Random.Range(0, goatWords.Count - 1)];
        wordText.color = Color.red;
        overlay = temp[1];
        AttackManager.enemyAttacks.Add(visibleBox);
    }
}
