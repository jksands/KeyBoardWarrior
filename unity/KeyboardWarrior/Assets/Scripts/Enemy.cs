using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Stores data on the enemy
    public string name = "bad-guy";
    public int maxHealth = 10;
    [SerializeField]
    private int health;

    // The textbox for the enemy's name
    public Text nameBox;
    // Displays the enemy's health, will change to a bar maybe?
    public Text healthBox;

    public Text attackBox;


    private void Start()
    {
        // Set the enemy's max health to it's given health
        health = maxHealth;

        // Set the enemy's info to display
        nameBox.text = name;
        healthBox.text = health + "/" + maxHealth + " HP";
        healthBox.color = Color.green;
    }

    /// <summary>
    /// Does said amount of damage to the enemy
    /// </summary>
    /// <param name="damageTaken"></param>
    public void DoDamage(int damageTaken)
    {
        health -= damageTaken;

        // Make the enemy invisible if he isn't already when dead
        if (gameObject.activeSelf && health <= 0)
        {
            gameObject.SetActive(false);

            nameBox.gameObject.SetActive(false);
            healthBox.gameObject.SetActive(false);
        }


        // Change the enemy's displayed health
        healthBox.text = health + "/" + maxHealth + " HP";

        // Set the color of the enemy's health based on how much they have left
        float healthPercent = (float)health / (float)maxHealth;

        if (healthPercent > .5f)
            healthBox.color = Color.green;
        else if (healthPercent > .3f)
            healthBox.color = Color.yellow;
        else
            healthBox.color = Color.red;
    }

    public bool IsDead()
    {
        return health<=0;
    }
}
