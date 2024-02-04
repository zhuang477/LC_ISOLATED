using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    UserData save;
    ItemDatabase itemDatabase;
    public GameObject UI;
    public Image Armor_UI;
    public Image Weapon_UI;
    public Image Shield_UI;

    //
    public GameObject BackpackGrid;
    public Transform GridAndIconPlaced;
    public GameObject Icon;

    //fill the backpack grid only, if using for(int i), the grid will loop forever.

    float posX =50f;
    float posY =-50f;
    List<GameObject> SlotList =new List<GameObject>();

    bool BackPackInit;

    // Update is called once per frame
    void Update()
    {
        save =GameManager.Instance.currentSaving;
        itemDatabase =GameManager.Instance.itemDatabase;
        if(save.evaluable ==1){
            if(Input.GetKey(KeyCode.Tab)){
                //UI.SetActive(true);
            }else{
                //UI.SetActive(false);
            }

            //
            Armor_UI.sprite =itemDatabase.getItem(save.weapon_id).itemPaperDoll;
            Weapon_UI.sprite =itemDatabase.getItem(save.armor_id).itemPaperDoll;
            Shield_UI.sprite =itemDatabase.getItem(save.shield_id).itemPaperDoll;
            //

            //fill the background with default grid.
            //save ->slot
            if(BackPackInit ==false){
                for(int backpack_slot =0;backpack_slot <save.backpack.Count;backpack_slot++){
                    if(backpack_slot %5==0 && backpack_slot !=0){
                        posX =50f;
                        posY -=105f;
                    }
                    GameObject grid =Instantiate(BackpackGrid,GridAndIconPlaced);
                    if(save.backpack[backpack_slot] !=0){
                        GameObject Slot_Icon =Instantiate(Icon,grid.transform);
                        Image icon_image =Slot_Icon.GetComponent<Image>();
                        icon_image.sprite =itemDatabase.getItem(save.backpack[backpack_slot]).itemIcon;
                        Slot_Icon.GetComponent<IconID>().ID =itemDatabase.getItem(save.backpack[backpack_slot]).id;
                    }
                    RectTransform GridrectTransform =grid.GetComponent<RectTransform>();
                    GridrectTransform.anchoredPosition =new UnityEngine.Vector2(posX,posY);
                    posX +=105f;
                    SlotList.Add(grid);
                }
                BackPackInit =true;
            }


            //slot ->save, for dragging and replacing items in backpack UI.
            for(int i=0;i<save.backpack.Count;i++){
                if(SlotList[i].transform.GetComponentInChildren<IconID>() !=null){
                    save.backpack[i] =SlotList[i].transform.GetComponentInChildren<IconID>().ID;
                }else{
                    save.backpack[i] =0;
                }
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

    //save ->slot
    //will need bound check in the future.
    void AddItemToBackpack(){
        for(int i=0;i<save.backpack.Count;i++){
            if(save.backpack[i] !=0 && SlotList[i].transform.childCount ==0){
                GameObject Slot_Icon =Instantiate(Icon,SlotList[i].transform);
                Slot_Icon.GetComponent<Image>().sprite =itemDatabase.getItem(save.backpack[i]).itemIcon;
                Slot_Icon.GetComponent<IconID>().ID =save.backpack[i];
            }
        }
    }

}
