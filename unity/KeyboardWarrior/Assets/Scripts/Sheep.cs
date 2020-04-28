using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheep : Enemy
{
    // This has its own unique attack
    bool sheepAttacking;
    float timer;
    public TurnManager tm;
    public AttackManager am;
    public Canvas canvas;
    public GameObject textBox;
    private GameObject visibleBox;
    private Vector3 boxPos;
    public Text wordText;
    public Text overlay;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        sheepAttacking = false;
        visibleBox = Instantiate(textBox, transform.position, Quaternion.identity, canvas.transform);
        visibleBox.SetActive(false);
        boxPos = visibleBox.transform.position;
        Debug.Log("Start");

    }

    // Update is called once per frame
    void Update()
    {
        if (sheepAttacking)
        {
            timer += Time.deltaTime;
            if (sheepAttacking)
            {
                if (AttackManager.enemyAttacks.Count == 0)
                {
                    sheepAttacking = false;
                    timer = 0;
                    tm.ChangeTurn();
                    visibleBox.SetActive(false);
                    AttackManager.enemyAttacking = false;
                    AttackManager.enemyAttacks.Clear();
                    visibleBox.transform.position = boxPos;
                }
                else
                {
                    am.SheepAttack("baaaa");
                }
            }
        }
    }

    public override void Attack()
    {
        Debug.Log("Sheep Attacking");
        sheepAttacking = true;
        AttackManager.enemyAttacking = true;
        Debug.Log(gameObject.name);
        visibleBox.SetActive(true);
        Text[] temp = visibleBox.GetComponentsInChildren<Text>();
        wordText = temp[0];
        wordText.text = "ATTACKING";
        overlay = temp[1];
        AttackManager.enemyAttacks.Add(visibleBox);
        
    }
}
