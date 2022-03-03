using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 1f;
    [SerializeField] private float delayTime = 3f;
    private Quaternion angle;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy", spawnTime, delayTime);
        angle.Set(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateEnemy(){
        Instantiate(enemyPrefab, new Vector3 (Random.Range(0, 30), 1, Random.Range(0, 30)), angle);
    }
}
