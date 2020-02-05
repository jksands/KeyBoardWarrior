using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public GameObject bard;     // Holds the player
    private Player p1;          // Holds reference to player's player script
    public EnemyManager em;     // Enemy manager
    public GameObject attack;
    public Canvas canvas;
    private List<GameObject> attacks;

    public float timePool = 10;
    public float timer = 0;
    private int sheepToggle = 0;
    // Start is called before the first frame update
    void Start()
    {
        p1 = bard.GetComponent<Player>();
        timePool = 10;
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
            
            attacks.Add(Instantiate(attack, em.enemies[sheepToggle].transform.position, Quaternion.identity, canvas.transform));
            p1.WasAttacked(attacks[attacks.Count - 1]);
            sheepToggle++;
            sheepToggle %= temp;
            timer = 0;
        }
        Vector3 pos;
        foreach(GameObject a in attacks)
        {
            pos = a.transform.position;
            pos.x -= 1 * Time.deltaTime;
            a.transform.position = pos;
        }
    }
}
