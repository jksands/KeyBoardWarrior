using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public EnemyManager em;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called by the player when its turn is done
    public void ChangeTurn()
    {
        Debug.Log("Switching Turns");
        Player.playerTurn = !Player.playerTurn;
        Debug.Log("It is my turn: " + Player.playerTurn);
        if (!Player.playerTurn) em.CalculateEnemyTurn();
    }
    
    
}
