using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 15f;
    [SerializeField] private float rotationSpeed = 3f;
    
    private float playerAxisY;

    [SerializeField] private float shootCooldownTime = 2f;

    private float timePassed = 0f;
    private bool canShoot = true;

    [SerializeField] private Animator crossbowAnimator;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource audioPlayer;
    
    public static bool arrowShooted = false;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            Move(Vector3.forward);
            playerAnimator.SetBool("isWalking", true);
        }
        else{
            playerAnimator.SetBool("isWalking", false);
        }
        if(Input.GetKey(KeyCode.A)){
            Move(Vector3.left);
        }
        if(Input.GetKey(KeyCode.S)){
            Move(Vector3.back);
        }
        if(Input.GetKey(KeyCode.D)){
            Move(Vector3.right);
        }

        if(!canShoot){
            RestartCooldown();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && canShoot){
            Shoot();
        }

        Rotate();
    }

    private void Move(Vector3 direction){
        transform.Translate(direction * playerSpeed * Time.deltaTime);
    }

    private void Rotate(){
        playerAxisY += Input.GetAxis("Mouse X") * rotationSpeed;
        Quaternion angleX = Quaternion.Euler(0f, playerAxisY, 0f);
        transform.localRotation = angleX;
    }

    private void RestartCooldown(){
        if(timePassed >= shootCooldownTime){
            arrowShooted = false;
            canShoot = true;
            crossbowAnimator.SetBool("isShooting", false);
        }
        else{
            timePassed += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        crossbowAnimator.SetBool("isShooting", true);
        timePassed = 0f;
        canShoot = false;
        arrowShooted = true;
    }
}
