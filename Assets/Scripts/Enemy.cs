using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] protected EnemyData enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
        LookAtPlayer();
    }

    
    protected virtual void ChasePlayer()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        if(!(distance < enemyStats.MaxDistance))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyStats.Speed * Time.deltaTime);
        }
    }

    protected virtual void LookAtPlayer()
    {
        transform.LookAt(player.transform.position);
    }
}
