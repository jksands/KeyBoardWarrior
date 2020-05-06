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

    public List<string> alphabet;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 200;
        // Set the enemy's max health to it's given health
        health = maxHealth;

        // Set the enemy's info to display
        nameBox.text = gameObject.name;
        healthBox.text = health + "/" + maxHealth + " HP";
        healthBox.color = Color.green;

        alphabet.Add("a");
        alphabet.Add("b");
        alphabet.Add("c");
        alphabet.Add("d");
        alphabet.Add("e");
        alphabet.Add("f");
        alphabet.Add("g");
        alphabet.Add("h");
        alphabet.Add("i");
        alphabet.Add("j");
        alphabet.Add("k");
        alphabet.Add("l");
        alphabet.Add("m");
        alphabet.Add("n");
        alphabet.Add("o");
        alphabet.Add("p");
        alphabet.Add("q");
        alphabet.Add("r");
        alphabet.Add("s");
        alphabet.Add("t");
        alphabet.Add("u");
        alphabet.Add("v");
        alphabet.Add("w");
        alphabet.Add("x");
        alphabet.Add("y");
        alphabet.Add("z");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(int z)
    {
        Debug.Log("Boss Attacking");
        // sheepAttacking = true;
        AttackManager.enemyAttacking = true;
        Vector3 position = transform.position;
        position.y -= 1;
        position.x += 1;
        position.z = z;
        visibleBox = Instantiate(textBox, position, Quaternion.identity, canvas.transform);
        Text[] temp = visibleBox.GetComponentsInChildren<Text>();
        wordText = temp[0];
        wordText.text = alphabet[Random.Range(0, alphabet.Count - 1)];
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
            temp1.GetComponent<Sheep>().canvas = canvas;
        }
        else
        {
            temp1 = Instantiate(goatPrefab, pos1.position, Quaternion.identity, canvas.transform);
            temp1.name = "goatee";
            temp1.GetComponent<Goat>().canvas = canvas;
        }

        if (Random.Range(0, 2) > 0)
        {
            temp2 = Instantiate(sheepPrefab, pos2.position, Quaternion.identity, canvas.transform);
            temp2.name = "baad-guy";
            temp2.GetComponent<Sheep>().canvas = canvas;
        }
        else
        {
            temp2 = Instantiate(goatPrefab, pos2.position, Quaternion.identity, canvas.transform);
            temp2.name = "degoat";
            temp2.GetComponent<Goat>().canvas = canvas;
        }

        em.viableEnemies.Add(temp1.GetComponent<Enemy>());
        em.viableEnemies.Add(temp2.GetComponent<Enemy>());
        Debug.Log(em.viableEnemies.Count);
        p.ResetMenu();

    }
}
