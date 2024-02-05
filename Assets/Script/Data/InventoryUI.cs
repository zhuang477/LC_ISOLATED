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
    public Image Weapon_UI;
    public Image Armor_UI;
    public Image Shield_UI;

    //
    public GameObject BackpackGrid;
    public Transform GridAndIconPlaced;
    public GameObject Icon;

    public GameObject Equipping_ArmorSlot;
    public GameObject Equipping_WeaponSlot;
    public GameObject Equipping_ShieldSlot;
    public GameObject Equipping_ProjectileSlot;

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
                UI.SetActive(true);
            }else{
                UI.SetActive(false);
            }

            //
            //save ->slot
            Armor_UI.sprite =itemDatabase.getItem(save.armor_id).itemPaperDoll;
            Weapon_UI.sprite =itemDatabase.getItem(save.weapon_id).itemPaperDoll;
            Shield_UI.sprite =itemDatabase.getItem(save.shield_id).itemPaperDoll;
            //

            //fill the background with default grid, only work once(when enter the game).
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
                        Slot_Icon.GetComponent<IconID>().ItemID =itemDatabase.getItem(save.backpack[backpack_slot]).id;
                        Slot_Icon.GetComponent<IconID>().SlotID =backpack_slot;

                        //place the itom into equipping slot.
                        if(Slot_Icon.GetComponent<IconID>().ItemID ==save.weapon_id
                        || Slot_Icon.GetComponent<IconID>().ItemID ==save.armor_id
                        || Slot_Icon.GetComponent<IconID>().ItemID ==save.shield_id){
                            //equipping item:
                            //weapon slot: -1;
                            //Armor slot: -2;
                            //Shield slot :-3;
                            if(itemDatabase.getItem(Slot_Icon.GetComponent<IconID>().ItemID).itemType.Equals("Weapon")){
                                Slot_Icon.transform.SetParent(Equipping_WeaponSlot.transform);
                                Slot_Icon.GetComponent<IconID>().SlotID =-1;
                            }
                            if(itemDatabase.getItem(Slot_Icon.GetComponent<IconID>().ItemID).itemType.Equals("Armor")){
                                Slot_Icon.transform.SetParent(Equipping_ArmorSlot.transform);
                                Slot_Icon.GetComponent<IconID>().SlotID =-2;
                            }
                            if(itemDatabase.getItem(Slot_Icon.GetComponent<IconID>().ItemID).itemType.Equals("Shield")){
                                Slot_Icon.transform.SetParent(Equipping_ShieldSlot.transform);
                                Slot_Icon.GetComponent<IconID>().SlotID =-3;
                            }
                        }
                    }
                    RectTransform GridrectTransform =grid.GetComponent<RectTransform>();
                    GridrectTransform.anchoredPosition =new UnityEngine.Vector2(posX,posY);
                    posX +=105f;
                    SlotList.Add(grid);
                }
                BackPackInit =true;
            }
            
            
            //slot ->save, detect equipment.
            if(Equipping_WeaponSlot.transform.childCount !=0){
                save.weapon_id =Equipping_WeaponSlot.GetComponentInChildren<IconID>().ItemID;
                Weapon_UI.color =new UnityEngine.Vector4(1,1,1,1);
            }else{
                save.weapon_id =999;
                Weapon_UI.color =new UnityEngine.Vector4(1,1,1,0);
            }
            //
            if(Equipping_ArmorSlot.transform.childCount !=0){
                save.armor_id =Equipping_ArmorSlot.GetComponentInChildren<IconID>().ItemID;
                Armor_UI.color =new UnityEngine.Vector4(1,1,1,1);
            }else{
                save.armor_id =1000;
                Armor_UI.color =new UnityEngine.Vector4(1,1,1,0);
            }
            //
            if(Equipping_ShieldSlot.transform.childCount !=0){
                save.shield_id =Equipping_ShieldSlot.GetComponentInChildren<IconID>().ItemID;
                Shield_UI.color =new UnityEngine.Vector4(1,1,1,1);
            }else{
                save.shield_id =998;
                Shield_UI.color =new UnityEngine.Vector4(1,1,1,0);
            }

            //slot ->save, for dragging and replacing items in backpack UI.
            for(int i=0;i<save.backpack.Count;i++){
                if(SlotList[i].transform.GetComponentInChildren<IconID>() !=null){
                    save.backpack[i] =SlotList[i].transform.GetComponentInChildren<IconID>().ItemID;
                }else{
                    save.backpack[i] =0;
                }
            }

            //slot ->save,update item location based on slotID;
            /**
            for(int i=0;i<SlotList.Count;i++){
                if(SlotList[i].transform.childCount !=0){
                    if(SlotList[i].GetComponentInChildren<IconID>().SlotID !=i){
                        GameObject Icon =SlotList[i].transform.GetChild(0).gameObject;
                        Icon.transform.SetParent(SlotList[Icon.GetComponent<IconID>().SlotID].transform);
                    }
                }
            }**/

            //setting mask.
            GridAndIconPlaced.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,-posY+50f);
            //
        }else{

        }
    }

    void LateUpdate(){
        
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
                Slot_Icon.GetComponent<IconID>().ItemID =save.backpack[i];
            }
        }
    }

}
