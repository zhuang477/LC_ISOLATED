using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{

    //A userdata should store the data about:

    //the name of save
    public string saveName;

    //the level that user unlocked
    public List<int> level; //when pass a boss, level will add to notify player is able to enter new level.

    //the current XP.
    public int XP;

    //the current stats of user

        //hitpoint.
        public int hitpoint;
        //stanima
        public int stanima;
        //stanima after cost armor, SHOULD NOT BE DIRECTLY ASSIGN.
        public int actual_stanima;

        //the weapon that player holding.
        public int weapon_id;
        //the armor that player wearing.
        public int armor_id;

    //perk that user unlocked
    public List<int> perks_unlocked; //when a perk is unlocked, add the index of the perk into list.

    //whats inside backpack
    public string[] backpack; //need more details!

    //paperdoll(what player currently wear and hold)
}
