using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Enemy
{
    // This has its own unique attack
    bool sheepAttacking;
    float timer;
    public TurnManager tm;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        sheepAttacking = false;
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
            }
        }
    }

    public override void Attack()
    {
        Debug.Log("Sheep Attacking");
        sheepAttacking = true;
        
    }
}
