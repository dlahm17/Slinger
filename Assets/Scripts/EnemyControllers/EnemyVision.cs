using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// author: Davis Lahm
/// purpose: to send information to an Enemy module.
/// This information is entirely if the player enters the cone of vision of the enemy (though this is subject to change as it could eventually be a combination of collision and raycasting)
/// </summary>
public class EnemyVision : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        //if the collision is with a player, then it'll tell the enemy that it's now in combat.
        if (other.tag.Equals("Player"))
        {
            Debug.Log("REEEEEEEEEEEE PLAYER FOUND");
            enemy.changeState(enemyState.combat);
        }
    }
}
