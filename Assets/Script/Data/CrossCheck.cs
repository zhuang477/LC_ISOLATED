using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CrossCheck : MonoBehaviour
{
    public ItemDatabase itemDatabase;

    // Update is called once per frame
    void Update()
    {
        CheckingEquipment();
    }

    void CheckingEquipment(){
        UserData save=GameManager.Instance.currentSaving;
        if(save.evaluable ==1){
            Item Weapon =itemDatabase.getItem(save.weapon_id);
            Item Armor =itemDatabase.getItem(save.armor_id);

            //it may minus the debuff in the future.
            save.weapon_against_flesh =Weapon.stats_stringTodouble["Attack_Raw_Damage"] * Weapon.stats_stringTodouble["Attack_Against_Flesh"];
            save.weapon_against_armor =Weapon.stats_stringTodouble["Attack_Raw_Damage"] * Weapon.stats_stringTodouble["Attack_Against_Armor"];


            save.actual_stanima =save.stanima -Armor.stats_stringToint["Armor_Stanima_Cost"];
            save.armor_head =Armor.stats_stringTodouble["Armor_Head"];
            save.armor_torso =Armor.stats_stringTodouble["Armor_Torso"];
            save.armor_leg =Armor.stats_stringTodouble["Armor_Leg"];
        }
    }
}
