using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Currently only stores info on enemies, will later queue up enemy attacks that the player has to avoid
    public List<Enemy> enemies;
    // Used when picking a random enemy
    private List<Enemy> viableEnemies;

    public Enemy currentTarget;

    // Here incase we ever want to hide the enemies' names
    public bool showNames = true;

    private void Start()
    {
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
    }
}
