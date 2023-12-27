using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CrossCheck : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public GameObject player_inventory;
    public GameObject player_status;

    // Update is called once per frame
    void Update()
    {
        CheckingEquipment();
    }

    void CheckingEquipment(){
        UserData save=GameManager.Instance.currentSaving;
        //it means player now enter the game.
        if(save.evaluable ==1){
            Item Weapon =itemDatabase.getItem(save.weapon_id);
            player_inventory.transform.Find("Weapon_UI").GetComponent<SpriteRenderer>().sprite =Weapon.itemIcon;
            player_inventory.transform.Find("Weapon_PaperDoll").GetComponent<SpriteRenderer>().sprite =Weapon.itemPaperDoll;
            Item Armor =itemDatabase.getItem(save.armor_id);
            player_inventory.transform.Find("Armor_UI").GetComponent<SpriteRenderer>().sprite =Armor.itemIcon;
            player_inventory.transform.Find("Armor_PaperDoll").GetComponent<SpriteRenderer>().sprite =Armor.itemPaperDoll;

            //it may minus the debuff in the future.
            save.weapon_against_flesh =Weapon.stats_stringTodouble["Attack_Raw_Damage"] * Weapon.stats_stringTodouble["Attack_Against_Flesh"];
            save.weapon_against_armor =Weapon.stats_stringTodouble["Attack_Raw_Damage"] * Weapon.stats_stringTodouble["Attack_Against_Armor"];


            save.actual_stanima =save.stanima -Armor.stats_stringToint["Armor_Stanima_Cost"];
            save.armor_head =Armor.stats_stringTodouble["Armor_Head"];
            save.armor_torso =Armor.stats_stringTodouble["Armor_Torso"];
            save.armor_leg =Armor.stats_stringTodouble["Armor_Leg"];

            //see the inventory pages.
            if (Input.GetKeyDown(KeyCode.Tab)){
                player_inventory.SetActive(true);
            }
            if(Input.GetKeyUp(KeyCode.Tab)){
                player_inventory.SetActive(false);
            }
        }else{
            player_inventory.SetActive(false);
            //player_status.SetActive(false);
        }
    }
}
