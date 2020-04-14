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
    public Text wordText;
    public Text overlay;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        sheepAttacking = false;
        visibleBox = Instantiate(textBox, transform.position, Quaternion.identity, canvas.transform);
        visibleBox.SetActive(false);
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
                if (timer > 2)
                {
                    sheepAttacking = false;
                    timer = 0;
                    tm.ChangeTurn();
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
        Debug.Log(gameObject.name);
        visibleBox.SetActive(true);
        Text[] temp = visibleBox.GetComponentsInChildren<Text>();
        wordText = temp[0];
        wordText.text = "";
        overlay = temp[1];
        
    }
}
