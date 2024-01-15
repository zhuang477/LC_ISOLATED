using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //This is use to store the player's current info and player control.
    public static GameManager Instance;

    private void Awake(){
        Instance =this;
        DontDestroyOnLoad(this.gameObject);
    }

    //It is a transport only.
    public UserData currentSaving;

    //database
    public ItemDatabase itemDatabase;

    void Update(){
        //enter the game.
        if(currentSaving.evaluable ==1){
            //Update the status that cannot be directly assign.
            Item Weapon =itemDatabase.getItem(currentSaving.weapon_id);
            Item Armor =itemDatabase.getItem(currentSaving.armor_id);
            //it may minus the debuff in the future.
            currentSaving.weapon_against_flesh =Weapon.stats_stringTodouble["Attack_Raw_Damage"] * Weapon.stats_stringTodouble["Attack_Against_Flesh"];
            currentSaving.weapon_against_armor =Weapon.stats_stringTodouble["Attack_Raw_Damage"] * Weapon.stats_stringTodouble["Attack_Against_Armor"];


            currentSaving.actual_stanima =currentSaving.stanima -Armor.stats_stringToint["Armor_Stanima_Cost"];
            currentSaving.armor_head =Armor.stats_stringTodouble["Armor_Head"];
            currentSaving.armor_torso =Armor.stats_stringTodouble["Armor_Torso"];
            currentSaving.armor_leg =Armor.stats_stringTodouble["Armor_Leg"];
            //
        }
    }
}
