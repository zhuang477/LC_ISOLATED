using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item 
{
    public int id;
    public Sprite itemIcon;
    public Sprite itemPaperDoll;
    public string itemName;
    public string itemType;
    public string description;
    //public Sprite icon;
    public Dictionary<string, int> stats_stringToint =new Dictionary<string, int>();
    public Dictionary<string, string> stats_stringTostring =new Dictionary<string, string>();
    public Dictionary<string, double> stats_stringTodouble =new Dictionary<string, double>();

    public Item(int id, string itemName, Sprite itemIcon, Sprite itemPaperDoll, string itemType, string description, Dictionary<string,int> stats_stringToint
    ,Dictionary<string,string> stats_stringTostring, Dictionary<string, double> stats_stringTodouble){
        this.id =id;
        this.itemName =itemName;
        this.itemIcon =itemIcon;
        this.itemPaperDoll =itemPaperDoll;
        this.itemType =itemType;
        this.description =description;
        this.stats_stringToint =stats_stringToint;
        this.stats_stringTostring =stats_stringTostring;
        this.stats_stringTodouble =stats_stringTodouble;
    }

    public Item(Item item){
        this.id =item.id;
        this.itemName =item.itemName;
        this.itemIcon =item.itemIcon;
        this.itemPaperDoll =item.itemPaperDoll;
        this.itemType =item.itemType;
        this.description =item.description;
        this.stats_stringToint =item.stats_stringToint;
        this.stats_stringTostring =item.stats_stringTostring;
        this.stats_stringTodouble =item.stats_stringTodouble;
    }
}
