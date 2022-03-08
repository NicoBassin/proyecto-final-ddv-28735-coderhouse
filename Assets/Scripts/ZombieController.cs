using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        LookAtPlayerLerp();
    }

    
    private void ChasePlayer()
    {
        float distance = (player.transform.position - transform.position).magnitude;

        if(!(distance < minDistance))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void LookAtPlayerLerp()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
}
