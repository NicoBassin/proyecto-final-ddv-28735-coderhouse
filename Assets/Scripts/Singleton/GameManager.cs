using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;
    public event Action OnDamage;
    public int playerHP, playerScore;

    private void Awake() {
        if(gmInstance == null){
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
            playerHP = 5;
            playerScore = 0;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void TakeDamage(){
        OnDamage?.Invoke();
    }
}
