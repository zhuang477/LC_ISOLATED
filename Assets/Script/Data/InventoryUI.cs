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
    public GameObject BackpackGrid;
    public Transform GridAndIconPlaced;

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
            Armor_UI.sprite =itemDatabase.getItem(save.weapon_id).itemPaperDoll;
            Weapon_UI.sprite =itemDatabase.getItem(save.armor_id).itemPaperDoll;
            Shield_UI.sprite =itemDatabase.getItem(save.shield_id).itemPaperDoll;
            //

            //fill the background with default grid.
            if(BackPackInit ==false){
                for(int backpack_slot =0;backpack_slot <save.backpack.Count;backpack_slot++){
                    if(backpack_slot %5==0 && backpack_slot !=0){
                        posX =50f;
                        posY -=105f;
                    }
                        GameObject grid =Instantiate(BackpackGrid,GridAndIconPlaced);
                        Image Icon =grid.transform.Find("Icon").gameObject.GetComponent<Image>();
                    if(save.backpack[backpack_slot] !=0){
                        Icon.sprite =itemDatabase.getItem(save.backpack[backpack_slot]).itemIcon;
                    }
                        RectTransform GridrectTransform =grid.GetComponent<RectTransform>();
                        GridrectTransform.anchoredPosition =new UnityEngine.Vector2(posX,posY);
                        posX +=105f;
                        SlotList.Add(grid);
                }
                BackPackInit =true;
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
                Image Icon =SlotList[i].transform.Find("Icon").gameObject.GetComponent<Image>();
                Icon.sprite =itemDatabase.getItem(save.backpack[i]).itemIcon;
            }
        }
    }
}
