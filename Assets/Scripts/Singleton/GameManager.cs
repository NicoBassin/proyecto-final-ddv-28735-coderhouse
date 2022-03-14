using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;
    public bool damaged;
    public int life, score;

    private void Awake() {
        if(gmInstance == null){
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
            damaged = false;
            life = 5;
            score = 0;
        }
        else{
            Destroy(gameObject);
        }
    }
}
