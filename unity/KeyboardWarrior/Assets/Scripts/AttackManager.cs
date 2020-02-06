using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public SpriteRenderer bSprite;
    public GameObject bard;     // Holds the player
    private Player p1;          // Holds reference to player's player script
    public EnemyManager em;     // Enemy manager
    public GameObject attack;
    public Canvas canvas;
    private List<GameObject> attacks;

    public float timePool = 5;
    public float timer = 0;
    private int sheepToggle = 0;
    Vector3 sheepPos;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        p1 = bard.GetComponent<Player>();
        timePool = 5;
        attacks = new List<GameObject>();
        // p1.WasAttacked(em.enemies[0].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // p1.WasAttacked(bard);
        timer += Time.deltaTime;
        int temp = em.enemies.Count;
        // If there are more enemies, attacks happen faster
        if (timer > timePool / temp)
        {
            sheepPos = em.enemies[sheepToggle].transform.position;
            sheepPos.y += 5;
            attacks.Add(Instantiate(attack, sheepPos, Quaternion.identity, canvas.transform));
            p1.WasAttacked(attacks[attacks.Count - 1]);
            sheepToggle++;
            sheepToggle %= temp;
            timer = 0;
        }
        for (int i = 0; i < attacks.Count; i++)
        {
            pos = attacks[i].transform.position;
            pos.x -= 1 * Time.deltaTime;
            attacks[i].transform.position = pos;
            if (attacks[i].transform.position.x < bSprite.bounds.extents.x + bSprite.bounds.center.x)
            {
                // Make the player take ONE WHOLE DAMAGE!
                p1.TakeDamage(1);
                Destroy(attacks[i]);
                attacks.RemoveAt(i);
                i--;
            }
        }
        //foreach(GameObject a in attacks)
        //{
        //    pos = a.transform.position;
        //    pos.x -= 1 * Time.deltaTime;
        //    a.transform.position = pos;
        //}
    }

    public void RemoveAttack(GameObject attack)
    {
        attacks.Remove(attack);
        Destroy(attack);
    }
}
