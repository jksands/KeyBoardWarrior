using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    // Currently only stores info on enemies, will later queue up enemy attacks that the player has to avoid
    public List<Enemy> enemies;
    // Used when picking a random enemy
    public List<Enemy> viableEnemies;

    public AttackManager am;


    public Enemy currentTarget;

    public GameObject cursor;

    // Here incase we ever want to hide the enemies' names
    public bool showNames = true;

    private int attackCount;

    private void Start()
    {
        cursor.SetActive(false);
        viableEnemies = new List<Enemy>();

        foreach (Enemy e in enemies)
            viableEnemies.Add(e);
    }


    public void ToggleEnemyNames()
    {
        showNames = !showNames;

        foreach (Enemy e in enemies)
            e.nameBox.enabled = showNames;
    }

    // Changes the enemy's name color when targeted
    public void TargetEnemy(Enemy newTarget)
    {
        // Make sure the enmey is a valid target before targetting
        if (!viableEnemies.Contains(newTarget))
            return;

        if (!currentTarget)
        {
            currentTarget = newTarget;
            currentTarget.nameBox.color = Color.red;
        }
        else
        {
            currentTarget.nameBox.color = Color.white;
            currentTarget = newTarget;
            currentTarget.nameBox.color = Color.red;
        }
        cursor.SetActive(true);
        cursor.transform.position = currentTarget.gameObject.transform.position;
    }

    public void EndTarget()
    {
        if (currentTarget)
            currentTarget = null;
    }

    public Enemy GetRandomTarget()
    {
        return viableEnemies[Random.Range(0,viableEnemies.Count)];
    }

    public void DamageTarget(int damage)
    {
        if (currentTarget)
        {
            currentTarget.DoDamage(damage);
            if (currentTarget.IsDead())
            {
                viableEnemies.Remove(currentTarget);
                cursor.SetActive(false);
                EndTarget();
            }
        }
        else
        {
            Enemy target = GetRandomTarget();
            target.DoDamage(damage);

            if (target.IsDead())
                viableEnemies.Remove(target);
        }
        if (viableEnemies.Count == 0)
        {
            if (Map.activeIndex == 2)
            {
                // You win!
                SceneManager.LoadScene(7);
            }
            else
            {
                // Load the map again and increase progress
                Map.progress++;
                SceneManager.LoadScene(2);

            }
        }
    }

    public void CalculateEnemyTurn()
    {
        Enemy attacker;
        attacker = viableEnemies[Random.Range(0, viableEnemies.Count)];
        if (attacker is Sheep)
        {
            Debug.Log("sheep?");
            List<Sheep> sheeple = new List<Sheep>();
            // loop through and count how many sheep there are.
            foreach (Enemy e in viableEnemies)
            {
                if (e is Sheep)
                {
                    sheeple.Add((Sheep)e);
                }
            }
            am.sheeple = sheeple;
            am.sheepAttacking = true;

        }
    }
}
