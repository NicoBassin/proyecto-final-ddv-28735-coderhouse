using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : Enemy
{
    
    [SerializeField] private float flightHeight = 3f;
    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        if((player.transform.position - transform.position).magnitude > (enemyStats.MaxDistance+1)){
            if(transform.position.y < flightHeight){
                transform.Translate(Vector3.up * enemyStats.Speed * Time.deltaTime);
            }
        }
    }
}
