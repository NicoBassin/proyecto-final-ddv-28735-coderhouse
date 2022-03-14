using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsInit : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] GameObject powerUpParent;

    // Start is called before the first frame update
    void Start()
    {
        CreatePowerUps();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePowerUps(){
        for(int i = 0; i < 10; i++){
            Vector3 newPosition = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], newPosition, powerUpPrefabs[0].transform.rotation, powerUpParent.transform);
        }
    }
}
