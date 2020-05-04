using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheep : Enemy
{
    // float timer;
    // public TurnManager tm;
    // public AttackManager am;
    public Canvas canvas;
    public GameObject textBox;
    private GameObject visibleBox;
    private Vector3 boxPos;
    public Text wordText;
    public Text overlay;

    public List<string> sheepWords;

    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // visibleBox.SetActive(false);
        // boxPos = visibleBox.transform.position;
        Debug.Log("Start");

        sheepWords.Add("bleat up");
        sheepWords.Add("steel wool");
        sheepWords.Add("rollout");
        sheepWords.Add("baaaaa");
        sheepWords.Add("hot fuzz");
        sheepWords.Add("wake up sheeple");
        sheepWords.Add("old mcdonald");
        sheepWords.Add("steep sheep");
        sheepWords.Add("baaaaa");
        sheepWords.Add("baaaaa");

    }

    // Update is called once per frame
    //void Update()
    //{
    //    // Would choose an attack here.  Sheep default to a group attack
    //    if (sheepAttacking)
    //    {
    //        if (attackCount < 5)
    //        {
    //            timer += Time.deltaTime;
    //            if (timer >= 2)
    //            {
    //                timer = 0;
    //                // am.SheepAttack("baaaa");
    //            }
    //        }
    //        // Max attacks have been spawned, so check if turn has ended
    //        else
    //        {
    //            if (AttackManager.enemyAttacks.Count == 0)
    //            {
    //                sheepAttacking = false;
    //                timer = 0;
    //                tm.ChangeTurn();
    //                visibleBox.SetActive(false);
    //                AttackManager.enemyAttacking = false;
    //                AttackManager.enemyAttacks.Clear();
    //                visibleBox.transform.position = boxPos;
    //            }
    //        }

    //    }
    //}

    public override void Attack()
    {
        Debug.Log("Sheep Attacking");
        // sheepAttacking = true;
        AttackManager.enemyAttacking = true;
        Vector3 position = transform.position;
        position.y -= 1;
        position.x += 1;
        visibleBox = Instantiate(textBox, position, Quaternion.identity, canvas.transform);
        Text[] temp = visibleBox.GetComponentsInChildren<Text>();
        wordText = temp[0];
        wordText.text = sheepWords[Random.Range(0, sheepWords.Count - 1)];
        wordText.color = Color.red;
        overlay = temp[1];
        AttackManager.enemyAttacks.Add(visibleBox);
        
    }
}
