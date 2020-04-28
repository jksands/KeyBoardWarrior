﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // Static list of enemy attackboxes
    public static List<GameObject> enemyAttacks;
    public static bool enemyAttacking;
    public SpriteRenderer bSprite;
    public GameObject bard;     // Holds the player
    private Player p1;          // Holds reference to player's player script
    public EnemyManager em;     // Enemy manager
    public GameObject attack;
    public Canvas canvas;
    private List<GameObject> attacks;

    public float timePool = 5;
    public float timer = 0;
    public float attackSpeedMod = 1; //Multiplier for how fast attacks are coming out
    private int sheepToggle = 0;
    Vector3 sheepPos;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        enemyAttacks = new List<GameObject>();
        enemyAttacking = false;
        p1 = bard.GetComponent<Player>();
        timePool = 7;
        attacks = new List<GameObject>();
        // p1.WasAttacked(em.enemies[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        // p1.WasAttacked(bard);
        //timer += (Time.deltaTime * attackSpeedMod) ;
        //int temp = em.enemies.Count;
        // If there are more enemies, attacks happen faster
        //if (timer > timePool / temp)
        //{
        //    sheepToggle = Random.Range(0, em.viableEnemies.Count);
        //    sheepPos = em.viableEnemies[sheepToggle].transform.position;
        //    sheepPos.y += 4;
        //    attacks.Add(Instantiate(attack, sheepPos, Quaternion.identity, canvas.transform));
        //    p1.WasAttacked(attacks[attacks.Count - 1]);
        //    // Choose a random sheep to attack

        //    timer = 0;
        //}
        if (AttackManager.enemyAttacking)
        {
            for (int i = 0; i < AttackManager.enemyAttacks.Count; i++)
            {
                pos = AttackManager.enemyAttacks[i].transform.position;
                pos.x -= 1 * Time.deltaTime;
                AttackManager.enemyAttacks[i].transform.position = pos;
                if (AttackManager.enemyAttacks[i].transform.position.x < bSprite.bounds.extents.x + bSprite.bounds.center.x)
                {
                    // Make the player take ONE WHOLE DAMAGE!
                    p1.TakeDamage(1);
                    enemyAttacks[i].SetActive(false);
                    AttackManager.enemyAttacks.RemoveAt(i);
                    i--;
                }
            }
            foreach (GameObject a in enemyAttacks)
            {
                pos = a.transform.position;
                pos.x -= 1 * Time.deltaTime;
                a.transform.position = pos;
            }
        }
    }

    public void RemoveAttack(GameObject attack)
    {
        attacks.Remove(attack);
        Destroy(attack);
    }

    public void SheepAttack(string word)
    {

    }
}
