using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items =new List<Item>();

    void Awake(){
        BuildItemDatabase();
    }

    public Item GetItem(int id){
        return items.Find(item => item.id ==id);
    }

    void BuildItemDatabase(){
        items =new List<Item>(){
            //
            new Item(0, "Gladiator_Sword", "Weapon","A Gladiator_Sword",
            new Dictionary<string, int>{
                {"Attack_Stanima",10},
                {"Attack_Raw_Damage",50},
            },
            new Dictionary<string, string>{
                {"Weapon Type","Sword"}
            }),
            //

            //
            new Item(1, "Gladiator_Armor_Flax", "Armor","A Gladiator armor made by flax",
            new Dictionary<string, int>{
                {"Armor_Stanima_Cost",5},
                {"Armor_Head",50},
                {"Armor_Torso",10},
                {"Armor_Leg",10}
            },
            new Dictionary<string, string>{
                
            }),
            //

        };
    }

    /*
    //armor 
            //armor name
            //armor stanima cost
            //armor point in head
            //armor point in torso
            //armor point in leg

        //weapon player use
            //weapon name
            //weapon type
            //weapon stanima cost
            //raw damage of this weapon.
            //damage against flesh, SHOULD NOT BE DIRECTLY ASSIGN.
            //damage against armor, SHOULD NOT BE DIRECTLY ASSIGN.
            //percentage that stun enemy
            //attack speed (it may change by perk)
            //turn around speed (the longer the arms the longer the turn around speed)
    */
}
