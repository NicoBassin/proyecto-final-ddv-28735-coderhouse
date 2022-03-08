using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] arrowPrefab;
    private Transform playerTransform;
    private bool recentlyShooted = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.arrowShooted && !recentlyShooted){
            Invoke("CreateArrow", 0.8f);
            recentlyShooted = true;
        }
        if(!PlayerController.arrowShooted && recentlyShooted){
            recentlyShooted = false;
        }
    }

    private void CreateArrow(){
        Instantiate(arrowPrefab[Random.Range(0, 3)], transform.position, playerTransform.rotation);
    }
}
