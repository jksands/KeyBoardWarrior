using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public Canvas canvas;
    public GameObject textBox;
    private GameObject visibleBox;
    private Vector3 boxPos;
    public Text wordText;
    public Text overlay;

    public EnemyManager em;

    public GameObject sheepPrefab;
    public GameObject goatPrefab;

    public Transform pos1;
    public Transform pos2;

    public Player p;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 300;
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

    public override void Attack()
    {
        Debug.Log("Boss Attacking");
        // sheepAttacking = true;
        AttackManager.enemyAttacking = true;
        Vector3 position = transform.position;
        position.y -= 1;
        position.x += 1;
        visibleBox = Instantiate(textBox, position, Quaternion.identity, canvas.transform);
        Text[] temp = visibleBox.GetComponentsInChildren<Text>();
        wordText = temp[0];
        // wordText.text = goatWords[Random.Range(0, goatWords.Count - 1)];
        wordText.color = Color.red;
        overlay = temp[1];
        AttackManager.enemyAttacks.Add(visibleBox);
    }

    public void CallToArms()
    {
        GameObject temp1;
        GameObject temp2;
        if (Random.Range(0, 2) > 0)
        {
            temp1 = Instantiate(sheepPrefab, pos1.position, Quaternion.identity, canvas.transform);
            temp1.name = "trevor";
        }
        else
        {
            temp1 = Instantiate(goatPrefab, pos1.position, Quaternion.identity, canvas.transform);
            temp1.name = "goatee";
        }
        if (Random.Range(0, 2) > 0)
        {
            temp2 = Instantiate(sheepPrefab, pos2.position, Quaternion.identity, canvas.transform);
            temp2.name = "baad-guy";
        }
        else
        {
            temp2 = Instantiate(goatPrefab, pos2.position, Quaternion.identity, canvas.transform);
            temp2.name = "degoat";
        }

        em.viableEnemies.Add(temp1.GetComponent<Enemy>());
        em.viableEnemies.Add(temp2.GetComponent<Enemy>());
        Debug.Log(em.viableEnemies.Count);
        p.ResetMenu();

    }
}
