using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private float rayDistance = 60f;
    [SerializeField] private GameObject dartPrefab;
    private bool canShoot = true;
    [SerializeField] private float cooldownTime = 2f;
    private float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot){
            DartTrapRaycastEmit();
        }
        else{
            timePassed += Time.deltaTime;
            if(timePassed > cooldownTime){
                canShoot = true;
            }
        }
    }

    private void DartTrapRaycastEmit(){
        RaycastHit hit;
        if(Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance)){
            if(hit.transform.CompareTag("Player")){
                canShoot = false;
                timePassed = 0f;
                GameObject b = Instantiate(dartPrefab, shootPoint.transform.position, dartPrefab.transform.rotation);
            }
        }
    }
    private void OnDrawGizmos() {
        if(canShoot){
            Gizmos.color = Color.magenta;
            Vector3 puntoB = shootPoint.transform.TransformDirection(Vector3.forward) * rayDistance;
            Gizmos.DrawRay(shootPoint.transform.position, puntoB);
        }
    }

    public void TrapState(){
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        Debug.Log("Evento OnButtonPush recibido por: DartTrap.TrapState()");
    }
}
