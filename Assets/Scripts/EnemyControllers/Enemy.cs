using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState { idle, searching, combat, fleeing };
public class Enemy : MonoBehaviour
{
    public enemyState myState = enemyState.idle;
    player_Health pHp;
    enemyHealth myHp;
    public EnemyMovement myMovement;
    // Start is called before the first frame update
    public virtual void Start()
    {
        myMovement = GetComponent<EnemyMovement>();
        myHp = GetComponent<enemyHealth>();
    }
    
    public void changeState(enemyState newState)
    {
        myState = newState;
    }
}
