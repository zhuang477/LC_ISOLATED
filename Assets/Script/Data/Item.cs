using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public int id;
    public string itemName;
    public string itemType;
    public string description;
    //public Sprite icon;
    public Dictionary<string, int> stats_stringToint =new Dictionary<string, int>();
    public Dictionary<string, string> stats_stringTostring =new Dictionary<string, string>();

    public Item(int id, string itemName, string itemType, string description, Dictionary<string,int> stats_stringToint
    ,Dictionary<string,string> stats_stringTostring){
        this.id =id;
        this.itemName =itemName;
        this.itemType =itemType;
        this.description =description;
        this.stats_stringToint =stats_stringToint;
        this.stats_stringTostring =stats_stringTostring;
    }

    public Item(Item item){
        this.id =item.id;
        this.itemName =item.itemName;
        this.itemType =item.itemType;
        this.description =item.description;
        this.stats_stringToint =item.stats_stringToint;
        this.stats_stringTostring =item.stats_stringTostring;
    }
}
