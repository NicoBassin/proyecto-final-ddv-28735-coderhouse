using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 15f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Material colorMaterial;
    
    private float playerAxisY;
    private CharacterController ccPlayer;
    private float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    private float jumpVelocity;
    private Vector3 speed;
    [SerializeField] private float shootCooldownTime = 2f;

    private float timePassed = 0f;
    private bool canShoot = true;

    [SerializeField] private Animator crossbowAnimator;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip crossbowSound;
    
    public static bool arrowShooted = false;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private float rayDistance = 50f;

    private bool canShootRaycast = true;
    [SerializeField] private float cooldownTimeRaycast = 2f;
    private float timePassedRaycast = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        ccPlayer = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gmInstance.damaged)
        {
            playerSpeed /= 2;
            GameManager.gmInstance.damaged = false;
            GameManager.gmInstance.life--;
            GameManager.gmInstance.score--;
            HUDManager.hudmInstance.LifeTextChange(GameManager.gmInstance.life);
        }
        if (GameManager.gmInstance.life == 0)
        {
            SceneManager.LoadScene("Level1");
            GameManager.gmInstance.life = 5;
            HUDManager.hudmInstance.LifeTextChange(GameManager.gmInstance.life);
        }

        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.forward);
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(Vector3.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.back);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ccPlayer.isGrounded)
        {
            Jump();
        }

        MoveCC();

        if (!canShoot)
        {
            RestartCooldown();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Shoot();
        }

        Rotate();
        ShowScore();

        if(GameObject.FindGameObjectsWithTag("PowerUp").Length == 0){
            GameManager.gmInstance.score+=50;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Siguiente nivel");
        }
        if(GameManager.gmInstance.score > 500){
            Debug.Log("Llegaste a 500 puntos!");
        }
        if(canShootRaycast){
            PlayerRaycastEmit();
        }
        else{
            timePassedRaycast += Time.deltaTime;
            if(timePassedRaycast > cooldownTimeRaycast){
                canShootRaycast = true;
            }
        }
    }

    private void MoveCC()
    {
        speed.y += gravity * Time.deltaTime;
        ccPlayer.Move(speed * Time.deltaTime);
        ccPlayer.Move(Vector3.down * Time.deltaTime);
    }

    private void MovePlayer(Vector3 direction){
        ccPlayer.Move(playerSpeed * Time.deltaTime * transform.TransformDirection(direction));
    }

    private void Jump(){
        ccPlayer.Move(jumpVelocity * Vector3.up);
        speed.y = Mathf.Sqrt(-gravity * jumpHeight);
    }

    private void Rotate(){
        playerAxisY += Input.GetAxis("Mouse X") * rotationSpeed;
        Quaternion angleX = Quaternion.Euler(0f, playerAxisY, 0f);
        transform.localRotation = angleX;
    }

    private void RestartCooldown(){
        if(timePassed >= shootCooldownTime){
            canShoot = true;
            crossbowAnimator.SetBool("isShooting", false);
        }
        else{
            timePassed += Time.deltaTime;
            if(arrowShooted){
                arrowShooted = false;
            }
        }
    }

    private void Shoot()
    {
        audioPlayer.PlayOneShot(crossbowSound, 0.75f);
        crossbowAnimator.SetBool("isShooting", true);
        timePassed = 0f;
        canShoot = false;
        arrowShooted = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy")){
            GameManager.gmInstance.damaged = true;
        }
        /*if(other.gameObject.CompareTag("PowerUp")){
            GameManager.gmInstance.score += 5;
            Destroy(other.gameObject);
        }*/
        if(other.gameObject.CompareTag("Dart")){
            GameManager.gmInstance.damaged = true;
            Destroy(other.gameObject);
        }
    }
    private void ShowScore(){
        Debug.Log("Tienes " + GameManager.gmInstance.score + " puntos!");
    }

    private void PlayerRaycastEmit(){
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance)){
            if(hit.transform.CompareTag("PowerUp")){
                HUDManager.hudmInstance.NewObject(hit.transform.gameObject);
                GameManager.gmInstance.score += 5;
                canShootRaycast = false;
                timePassedRaycast = 0f;
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
