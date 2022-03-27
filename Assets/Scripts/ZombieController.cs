using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    protected override void Update()
    {
        base.Update();
        StayGrounded();
    }
    protected override void LookAtPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, enemyStats.RotationSpeed * Time.deltaTime);
    }

    private void StayGrounded(){
        if(transform.position.y > 0){
            transform.Translate(Vector3.down * enemyStats.Speed * Time.deltaTime);
        }
    }
}
