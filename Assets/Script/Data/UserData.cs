using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{

    //A userdata should store the data about:

    //Is read/write file allowed?
    public int evaluable;

    //the name of save
    public string saveName;

    //the level that user unlocked
    public List<int> level; //when pass a boss, level will add to notify player is able to enter new level.

    //the current XP.
    public int XP;

    //the current stats of user

        //hitpoint.
        public double hitpoint;
        //stanima
        public int stanima;
        //stanima after cost armor, SHOULD NOT BE DIRECTLY ASSIGN.
        public int actual_stanima;

        //the weapon that player holding.
        public int weapon_id;
        public double weapon_against_flesh;
        public double weapon_against_armor;
        //the armor that player wearing.
        public int armor_id;
        public double armor_head;
        public double armor_torso;
        public double armor_leg;

    //perk that user unlocked
    public List<int> perks_unlocked; //when a perk is unlocked, add the index of the perk into list.

    //whats inside backpack
    public List<int> backpack; //store the item id.

    public List<int> debuff; //debuff can not only one.

    //paperdoll(what player currently wear and hold)
}
