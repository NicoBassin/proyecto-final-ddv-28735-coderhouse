using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerStats;
    private float movementSpeed;
    private float playerAxisY;
    private CharacterController ccPlayer;
    private float gravity = -9.81f;
    private float jumpVelocity;
    private Vector3 speed;

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

    public event Action OnDeath;

    void Awake(){
        FindObjectOfType<PlayerController>().OnDeath += Die;
        FindObjectOfType<GemsCounter>().OnPowerUpsCollected += NextLevel;
        FindObjectOfType<GameManager>().OnDamage += Damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = playerStats.PlayerSpeed;
        audioPlayer = GetComponent<AudioSource>();
        ccPlayer = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
        MoveCC();
        Shoot();
        Rotate();
        ShowScore();
        PlayerRaycastEmit();

        if(GameManager.gmInstance.playerHP <= 0){
            OnDeath?.Invoke();
            Debug.Log("Evento OnDeath llamado por: PlayerController");
        }
    }

    private void Walk()
    {
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
    }

    private void MoveCC()
    {
        speed.y += gravity * Time.deltaTime;
        ccPlayer.Move(speed * Time.deltaTime);
        ccPlayer.Move(Vector3.down * Time.deltaTime);
    }

    private void Damage()
    {
        movementSpeed /= 2;
        GameManager.gmInstance.playerHP--;
        GameManager.gmInstance.playerScore--;
        HUDManager.hudmInstance.LifeTextChange(GameManager.gmInstance.playerHP);
    }

    private void MovePlayer(Vector3 direction){
        ccPlayer.Move(movementSpeed * Time.deltaTime * transform.TransformDirection(direction));
    }

    private void Jump(){
        if (Input.GetKeyDown(KeyCode.Space) && ccPlayer.isGrounded)
        {
            ccPlayer.Move(jumpVelocity * Vector3.up);
            speed.y = Mathf.Sqrt(-gravity * playerStats.JumpHeight);
        }
    }

    private void Rotate(){
        playerAxisY += Input.GetAxis("Mouse X") * playerStats.RotationSpeed;
        Quaternion angleX = Quaternion.Euler(0f, playerAxisY, 0f);
        transform.localRotation = angleX;
    }

    private void Cooldown(){
        if (!canShoot)
        {
            if(timePassed >= playerStats.ShootCooldownTime){
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
    }

    private void Shoot()
    {
        Cooldown();
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            audioPlayer.PlayOneShot(crossbowSound, 0.75f);
            crossbowAnimator.SetBool("isShooting", true);
            timePassed = 0f;
            canShoot = false;
            arrowShooted = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy")){
            GameManager.gmInstance.TakeDamage();
        }
        if(other.gameObject.CompareTag("Dart")){
            GameManager.gmInstance.TakeDamage();
            Destroy(other.gameObject);
        }
    }
    private void ShowScore(){
        Debug.Log("Tienes " + GameManager.gmInstance.playerScore + " puntos!");
        if(GameManager.gmInstance.playerScore > 500){
            Debug.Log("Llegaste a 500 puntos!");
        }
    }

    private void NextLevel()
    {
        Debug.Log("Evento OnGemsColledted recibido por: PlayerController.NextLevel()");
        GameManager.gmInstance.playerScore += 50;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Siguiente nivel");
    }
    private void PlayerRaycastEmit(){
        if (canShootRaycast)
        {
            RaycastHit hit;
            if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance)){
                if(hit.transform.CompareTag("PowerUp")){
                    HUDManager.hudmInstance.NewObject(hit.transform.gameObject);
                    GameManager.gmInstance.playerScore += 5;
                    canShootRaycast = false;
                    timePassedRaycast = 0f;
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        else
        {
            timePassedRaycast += Time.deltaTime;
            if (timePassedRaycast > cooldownTimeRaycast)
            {
                canShootRaycast = true;
            }
        }
    }

    private void Die()
    {
        Debug.Log("Evento OnDeath recibido por: PlayerController.Die()");
        SceneManager.LoadScene("Level1");
        Debug.Log("Moriste! D:");
        movementSpeed = 5f;
        GameManager.gmInstance.playerHP = 5;
        GameManager.gmInstance.playerScore = 0;
        HUDManager.hudmInstance.LifeTextChange(GameManager.gmInstance.playerHP);
    }
}
