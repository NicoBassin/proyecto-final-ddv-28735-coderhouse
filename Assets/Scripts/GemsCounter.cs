using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemsCounter : MonoBehaviour
{
    public event Action OnPowerUpsCollected;
    private int powerUpsNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerUpsNumber = GetGemsNumber();
        if(powerUpsNumber == 0){
            OnPowerUpsCollected?.Invoke();
            Debug.Log("Evento OnGemsCollected llamado por: GemsCounter");
        }
    }

    int GetGemsNumber(){
        return GameObject.FindGameObjectsWithTag("PowerUp").Length;
    }
}
