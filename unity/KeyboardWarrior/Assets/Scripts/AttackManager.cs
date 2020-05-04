using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float attackSpeedMod = 1.5f; //Multiplier for how fast attacks are coming out
    private int sheepToggle = 0;
    Vector3 sheepPos;
    Vector3 pos;

    // Sheep Stuff
    public bool sheepAttacking;
    public List<Sheep> sheeple;
    private int attackCount;
    public int lastSheep;

    public GameObject activeAttack;
    public Text activeText;
    public Text activeOverlay;
    public int activeIndex;

    // Goat Stuff
    public bool goatAttacking;
    public List<Goat> goats;

    // Boss Stuff
    public bool bossAttacking;
    public Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        enemyAttacks = new List<GameObject>();
        enemyAttacking = false;
        p1 = bard.GetComponent<Player>();
        timePool = 7;
        attacks = new List<GameObject>();
        activeIndex = 0;
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
                pos.x -= attackSpeedMod * Time.deltaTime;
                AttackManager.enemyAttacks[i].transform.position = pos;
                if (AttackManager.enemyAttacks[i].transform.position.x < bSprite.bounds.extents.x + bSprite.bounds.center.x)
                {
                    // Make the player take ONE WHOLE DAMAGE!
                    p1.TakeDamage(1);
                    // enemyAttacks[i].SetActive(false);
                    if (enemyAttacks[i] == activeAttack)
                        DestroyActive();
                    else
                    {
                        Destroy(enemyAttacks[i]);
                        enemyAttacks.RemoveAt(i);
                    }
                    i--;
                }
            }
            //foreach (GameObject a in enemyAttacks)
            //{
            //    pos = a.transform.position;
            //    pos.x -= 1 * Time.deltaTime;
            //    a.transform.position = pos;
            //}
        }

        // Unique code for sheep attacking
        if (sheepAttacking)
        {
            SheepAttack();
        }
        // Unique code for goat attacking
        if (goatAttacking)
        {
            GoatAttack();
        }
        if (bossAttacking)
        {
            BossAttack();
        }
    }

    public void ChangeActiveAttack(int index)
    {
        // If there are no attacks to change to; do nothing
        if (enemyAttacks.Count < 2) return;

        Text[] temp;
        if (activeAttack != null)
        {
            temp = activeAttack.GetComponentsInChildren<Text>();
            temp[0].color = Color.red;
            activeOverlay.text = "";
        }
        activeAttack = enemyAttacks[index %= enemyAttacks.Count];
        temp = activeAttack.GetComponentsInChildren<Text>();
        temp[0].color = Color.white;
        activeText = temp[0];
        activeOverlay = temp[1];
        activeIndex = index;

    }

    public void SetActiveAttack(int index)
    {
        Debug.Log("Setting first boi 2");
        Text[] temp;
        activeAttack = enemyAttacks[index %= enemyAttacks.Count];
        temp = activeAttack.GetComponentsInChildren<Text>();
        temp[0].color = Color.white;
        activeIndex = index;
        activeText = temp[0];
        activeOverlay = temp[1];
    }
    public void RemoveAttack(GameObject attack)
    {
        attacks.Remove(attack);
        Destroy(attack);
    }

    public void DestroyActive()
    {
        Destroy(activeAttack);
        enemyAttacks.Remove(activeAttack);
        if (enemyAttacks.Count > 0)
        SetActiveAttack(activeIndex % enemyAttacks.Count);
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
                int thisSheep = Random.Range(0, sheeple.Count);
                if (thisSheep == lastSheep) { thisSheep++; thisSheep %= sheeple.Count; }
                sheeple[thisSheep].Attack();
                lastSheep = thisSheep;
                Debug.Log("Setting first boi 1");
                if (enemyAttacks.Count == 1)
                {
                    Debug.Log("Setting first boi 1.5");
                    SetActiveAttack(0);
                }
            }
        }
        // Max attacks have been spawned, so check if turn has ended
        else
        {
            if (enemyAttacks.Count == 0)
            {
                Debug.Log("TURN ENDING");
                attackCount = 0;
                activeAttack = null;
                activeIndex = 0;
                sheepAttacking = false;
                timer = 0;
                enemyAttacking = false;
                enemyAttacks.Clear();
                tm.ChangeTurn();
            }
        }

    }

    public void GoatAttack()
    {
        if (attackCount < 5)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                attackCount++;
                foreach (Goat g in goats)
                    g.Attack();
                if (enemyAttacks.Count <= 2)
                {
                    Debug.Log("Setting first boi 1.5");
                    SetActiveAttack(0);
                }
            }
        }
        // Max attacks have been spawned, so check if turn has ended
        else
        {
            if (enemyAttacks.Count == 0)
            {
                Debug.Log("TURN ENDING");
                attackCount = 0;
                activeAttack = null;
                activeIndex = 0;
                goatAttacking = false;
                timer = 0;
                enemyAttacking = false;
                enemyAttacks.Clear();
                tm.ChangeTurn();
            }
        }
    }

    public void BossAttack()
    {
        if (em.viableEnemies.Count >= 1)
        {
            attackSpeedMod = 2;
            Debug.Log(em.viableEnemies.Count);
            if (attackCount < 20)
            {
                timer += Time.deltaTime;
                if (timer >= .2)
                {
                    timer = 0;
                    attackCount++;
                    boss.Attack(attackCount);
                    if (enemyAttacks.Count == 1)
                    {
                        Debug.Log("Setting first boi 1.5");
                        SetActiveAttack(0);
                    }
                }
            }
            // Max attacks have been spawned, so check if turn has ended
            else
            {
                if (enemyAttacks.Count == 0)
                {
                    Debug.Log("TURN ENDING");
                    attackCount = 0;
                    activeAttack = null;
                    activeIndex = 0;
                    bossAttacking = false;
                    timer = 0;
                    enemyAttacking = false;
                    enemyAttacks.Clear();
                    tm.ChangeTurn();
                }
            }
        }
        if (em.viableEnemies.Count == 1)
        {
            Debug.Log("Calling to Arms");
            boss.CallToArms();
            //Debug.Log("TURN ENDING");
            //attackCount = 0;
            //activeAttack = null;
            //activeIndex = 0;
            //goatAttacking = false;
            //timer = 0;
            //bossAttacking = false;
            //enemyAttacks.Clear();
            //tm.ChangeTurn();
        }
    }
}
