using System.Collections;
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
    public TurnManager tm;      // Turn Manager
    public GameObject attack;
    public Canvas canvas;
    private List<GameObject> attacks;

    public float timePool = 5;
    public float timer = 0;
    public float attackSpeedMod = 1; //Multiplier for how fast attacks are coming out
    private int sheepToggle = 0;
    Vector3 sheepPos;
    Vector3 pos;

    public bool sheepAttacking;
    public List<Sheep> sheeple;
    private int attackCount;
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
                    // enemyAttacks[i].SetActive(false);
                    Destroy(enemyAttacks[i]);
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

        // Unique code for sheep attacking
        if (sheepAttacking)
        {
            SheepAttack();
        }
    }

    public void RemoveAttack(GameObject attack)
    {
        attacks.Remove(attack);
        Destroy(attack);
    }

    // Triggers the sheep attack.
    public void SheepAttack()
    {
        if (attackCount < sheeple.Count * 2 - 1)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                attackCount++;
                sheeple[Random.Range(0, sheeple.Count)].Attack();
            }
        }
        // Max attacks have been spawned, so check if turn has ended
        else
        {
            if (enemyAttacks.Count == 0)
            {
                Debug.Log("TURN ENDING");
                attackCount = 0;
                sheepAttacking = false;
                timer = 0;
                enemyAttacking = false;
                enemyAttacks.Clear();
                tm.ChangeTurn();
            }
        }

    }
}
