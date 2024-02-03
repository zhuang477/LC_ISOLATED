using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    public GameObject BackpackIcon;
    public Transform GridAndIconPlaced;
    public Scrollbar backpack_scrollbar;
    //fill the backpack grid only, if using for(int i), the grid will loop forever.
    int backpack_slot=0;

    float posX =50f;
    float posY =-50f;
    List<GameObject> IconList =new List<GameObject>();

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
            for(;backpack_slot <save.backpack.Count;backpack_slot++){
                if(backpack_slot %5==0 && backpack_slot !=0){
                    posX =50f;
                    posY -=100f;
                }
                GameObject grid =Instantiate(BackpackGrid,GridAndIconPlaced);
                GameObject icon =Instantiate(BackpackIcon,GridAndIconPlaced);
                RectTransform GridrectTransform =grid.GetComponent<RectTransform>();
                RectTransform IconrectTransform =icon.GetComponent<RectTransform>();
                GridrectTransform.anchoredPosition =new UnityEngine.Vector2(posX,posY);
                IconrectTransform.anchoredPosition =new UnityEngine.Vector2(posX,posY);
                posX +=100f;
                IconList.Add(icon);
            }
            //

            
            int iconNumber =0;
            //init the backpack icon.
            if(IsBackpackInit ==false){
                foreach(GameObject iconGO in IconList){
                    Image imageComponent =iconGO.GetComponent<Image>();
                    if(itemDatabase.getItem(save.backpack[iconNumber]) ==null){

                    }else{
                        imageComponent.sprite =itemDatabase.getItem(save.backpack[iconNumber]).itemIcon;
                    }
                    iconNumber++;
                }
                IsBackpackInit =true;
            }
            //setting mask.
            GridAndIconPlaced.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,-posY+50f);
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
                Image IconCheck =IconList[i].GetComponent<Image>();
                if(IconCheck.sprite.name =="Blank"){
                    IconCheck.sprite =itemDatabase.getItem(save.backpack[i]).itemIcon;
                }
            }
        }
    }
}
