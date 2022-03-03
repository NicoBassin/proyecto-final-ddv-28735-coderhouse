using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject rotatingPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotatingPoint.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
