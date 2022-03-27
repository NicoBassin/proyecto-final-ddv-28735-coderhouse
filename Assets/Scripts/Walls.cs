using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWalls(){
        Destroy(this.gameObject);
        Debug.Log("Evento OnButtonPush recibido por: Walls.OpenWalls()");
    }
}
