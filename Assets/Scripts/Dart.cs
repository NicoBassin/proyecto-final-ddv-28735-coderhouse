using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField] private float dartSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDart();
    }

    private void MoveDart()
    {
        transform.Translate(Vector3.forward * dartSpeed * Time.deltaTime);
    }
}
