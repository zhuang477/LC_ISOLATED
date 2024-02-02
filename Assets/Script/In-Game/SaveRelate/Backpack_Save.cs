using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack_Save : MonoBehaviour
{
    UserData save;
    //this is for items that not being assign to the tempbackpack,
    //for example, pick up items. I should not directly add it to the TempBackpack
    //since Pickable.cs should not use for loop to find empty slots in TempBackpack.
    public List<int> pendingItem;

    public delegate void Backpack();
    public static event Backpack AddItem;

    // Update is called once per frame
    void Update()
    {
        save =GameManager.Instance.currentSaving;
        //when enter the game
        if(save.evaluable ==1){
            ItemAdded();
        }
    }

    void ItemAdded(){
        if(pendingItem.Count !=0){
            for(int i=0;i<save.backpack.Count;i++){
                if(save.backpack[i]==0){
                    //add items in tempbackpack.
                    save.backpack[i] =pendingItem[0];
                    if(AddItem !=null){
                        AddItem();
                    }
                    //remove item from pendingItem.
                    pendingItem.RemoveAt(0);
                    break;
                }
            }
        }
    }
}
