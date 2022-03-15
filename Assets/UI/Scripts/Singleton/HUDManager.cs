using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public enum ColorPowerUp {Blue, Green, Pink, Yellow};
    public static HUDManager hudmInstance;
    [SerializeField] GameObject[] panels;
    [SerializeField] Sprite[] imagesPrefabs;
    [SerializeField] private GameObject panel;
    List<Image> images = new List<Image>();
    public int index = 0;
    public int color = 0; // Blue = 0, Green = 1, Pink = 2, Yellow = 3
    public bool maxItems = false;

    void Awake(){
        if(hudmInstance == null){
            hudmInstance = this;
            DontDestroyOnLoad(gameObject);
            maxItems = false;
            index = 0;
            color = 0;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject panel in panels){
            images.Add(panel.transform.GetChild(0).GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewObject(GameObject item)
    {
        color = (int)item.GetComponent<ColorType>().color;
        panels[index].SetActive(true);
        images[index].sprite = imagesPrefabs[color];

        if(!maxItems){
            InventoryManager.imInstance.AddToInventory(item, index);
        }
        else{
            InventoryManager.imInstance.RemoveFromInventory(index);
            InventoryManager.imInstance.AddToInventory(item, index);
        }

        if(index < 7){
            index++;
        }
        else{
            index = 0;
            maxItems = true;
        }
    }

    public void ToggleStart(){
        
    }

    public void ToggleExit(){

    }
}
