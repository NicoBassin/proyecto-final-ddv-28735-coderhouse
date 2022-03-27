using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private float minSpawnDistance = 2f;
    private Quaternion angle;
    private Vector3 newEnemyPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("CreateEnemy", spawnTime, delayTime);
        angle.Set(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomizeNewEnemyPosition(){
        newEnemyPosition = new Vector3(Random.Range(0, 30), 0, Random.Range(0, 30));
        if((newEnemyPosition - player.transform.position).magnitude < minSpawnDistance){
            RandomizeNewEnemyPosition();
        }
    }

    private void CreateEnemy(){
        RandomizeNewEnemyPosition();
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], newEnemyPosition, angle);
    }
}
