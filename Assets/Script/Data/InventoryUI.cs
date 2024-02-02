using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    UserData save;
    ItemDatabase itemDatabase;
    public GameObject UI;
    public Image Armor_UI;
    public Image Weapon_UI;
    public Image Shield_UI;

    //
    public GameObject Backpack_BG;
    public GameObject BackpackGrid;
    //fill the backpack grid only, if using for(int i), the grid will loop forever.
    int backpack_slot=0;
    List<GameObject> gridList =new List<GameObject>();
    //

    bool IsBackpackInit =false;


    // Update is called once per frame
    void Update()
    {
        save =GameManager.Instance.currentSaving;
        itemDatabase =GameManager.Instance.itemDatabase;
        if(save.evaluable ==1){
            if(Input.GetKey(KeyCode.Tab)){
                UI.SetActive(true);
            }else{
                UI.SetActive(false);
            }

            //
            Armor_UI.sprite =itemDatabase.getItem(save.weapon_id).itemPaperDoll;
            Weapon_UI.sprite =itemDatabase.getItem(save.armor_id).itemPaperDoll;
            Shield_UI.sprite =itemDatabase.getItem(save.shield_id).itemPaperDoll;
            //

            //fill the background with default grid.
            float posX =-240f;
            float posY =50f;
            for(;backpack_slot <save.backpack.Count;backpack_slot++){
                if(backpack_slot %5==0 && backpack_slot !=0){
                    posX =-240f;
                    posY -=100f;
                }
                GameObject grid =Instantiate(BackpackGrid,Backpack_BG.transform);
                RectTransform rectTransform =grid.GetComponent<RectTransform>();
                rectTransform.anchoredPosition =new Vector2(posX,posY);
                posX +=100f;
                gridList.Add(grid);
            }
            //

            
            int gridNumber =0;
            //init the backpack icon.
            if(IsBackpackInit ==false){
                foreach(GameObject iconGO in gridList){
                    Image imageComponent =iconGO.GetComponent<Image>();
                    if(itemDatabase.getItem(save.backpack[gridNumber]) ==null){

                    }else{
                        imageComponent.sprite =itemDatabase.getItem(save.backpack[gridNumber]).itemIcon;
                    }
                    gridNumber++;
                }
                IsBackpackInit =true;
            }
            //
        }else{

        }
    }

    void OnEnable(){
        Backpack_Save.AddItem += AddItemToBackpack;
    }

    void OnDisable(){
        Backpack_Save.AddItem -= AddItemToBackpack;
    }

    
    void AddItemToBackpack(){
        for(int i=0;i<save.backpack.Count;i++){
            if(save.backpack[i] !=0){
                Image IconCheck =gridList[i].GetComponent<Image>();
                if(IconCheck.sprite ==null){
                    IconCheck.sprite =itemDatabase.getItem(save.backpack[i]).itemIcon;
                }
            }
        }
    }
}
