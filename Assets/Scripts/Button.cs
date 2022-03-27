using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private UnityEvent OnButtonPush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            OnButtonPush?.Invoke();
            Debug.Log("Evento OnButtonPush llamado por: Button");
            if(this.gameObject.name == "Button 2"){
                Debug.Log("Evento OnButtonPush recibido por: AudioSource.Pause()");
            }
        }
    }
}
