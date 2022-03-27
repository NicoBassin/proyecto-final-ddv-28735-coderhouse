using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArrowColor{
    Blue,
    Red,
    Green
}

public class Arrow : MonoBehaviour
{
    private float arrowSpeed;
    private GameObject enemy;
    private bool enemyImpaled = false;
    private bool canMove = true;
    [SerializeField] private ArrowColor arrowName;

    // Start is called before the first frame update
    void Start()
    {
        switch(arrowName){
            case ArrowColor.Green:
                arrowSpeed = 15f;
                break;
            case ArrowColor.Blue:
                arrowSpeed = 20f;
                break;
            case ArrowColor.Red:
                arrowSpeed = 25f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyImpaled){
            MoveEnemy();
        }
        if(canMove){
            MoveArrow();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyImpaled = true;
            enemy = other.gameObject;
        }
    }

    private void MoveEnemy()
    {
        enemy.transform.localRotation = transform.localRotation;
        enemy.transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Wall")){
            enemyImpaled = false;
            canMove = false;
            Invoke("DestroyGameObjects", 5f);
            GameManager.gmInstance.playerScore++;
        }
    }

    private void MoveArrow()
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }

    private void DestroyGameObjects(){
        if(enemy != null){
            Destroy(enemy.gameObject);
        }
        Destroy(gameObject);
    }
}
