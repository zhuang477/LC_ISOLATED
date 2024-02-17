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

    
    public Vector3 location; //Location should based on scene's individual script's assign.

    //current level and nextlevel should be assign by individual scene script.
    public int currentLevel; //where the player is, it is spawnpoint that player spawn in.
    
    //
    public int moveSpeed;
    
    //the current XP.
    public int XP;

    //the current stats of user

        //hitpoint.
        public double hitpoint;
        //stanima
        public int stanima;
        public double recovery;
        //stanima after cost armor, SHOULD NOT BE DIRECTLY ASSIGN.
        public int actual_stanima;

        //special action, such as dodge, parry, unique attack animation needs action point.
        public int action_point;

        //the weapon that player holding.
        public int weapon_id;
        public int weapon_attack_stanima_cost;
        public double weapon_against_flesh;
        public double weapon_against_armor;
        //the armor that player wearing.
        public int armor_id;
        public double armor_head;
        public double armor_torso;
        public double armor_leg;

        //the shield player holding.
        public int shield_id;

    //perk that user unlocked
    public List<int> perks_unlocked; //when a perk is unlocked, add the index of the perk into list.
    //whats inside backpack
    public List<int> backpack; //store the item id.
    public int itemInBackpack; //directly find how many items in backpack, mainly for TempSave.

    public List<int> debuff; //debuff can not only one.

    //temporary status.
    public double current_hitpoint;
    public double current_stanima;
    public double current_recovery;
}
