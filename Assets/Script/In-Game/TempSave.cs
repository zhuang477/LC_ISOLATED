using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TempSave : MonoBehaviour
{
    UserData save;
    double TempHP;
    public List<int> TempBackpack;
    public List<int> TempItemLocation;

    //this is for items that not being assign to the tempbackpack,
    //for example, pick up items. I should not directly add it to the TempBackpack
    //since Pickable.cs should not use for loop to find empty slots in TempBackpack.
    public List<int> pendingItem;
    public int ItemCount =0;

    //increase the backpack size is diff from init backpack size,
    //dont use this when backpack size increased!
    bool IsTempBackPackInit =false;

    // Update is called once per frame
    void Update()
    {
        save =GameManager.Instance.currentSaving;
        //when the game start.
        if(save.evaluable ==1){
            //the size of temp backpack is equal to backpack size -in bag items size.
            if(IsTempBackPackInit ==false){
                TempBackpack =Enumerable.Repeat(0, save.backpack.Count -save.itemInBackpack).ToList();
                IsTempBackPackInit =true;
            }
            //detecting pedingItem list.
            if(pendingItem.Count !=0){
                for(int i=0;i<TempBackpack.Count;i++){
                    if(TempBackpack[i]==0){
                        //add items in tempbackpack.
                        TempBackpack[i] =pendingItem[0];
                        //add item count.
                        ItemCount++;
                        //remove item from pendingItem.
                        pendingItem.Remove(0);
                    }
                }
                //shows the item will be handled in InventoryUI.cs.
            }
        }
    }

    void OnEnable(){
        Teleport.saveGame +=Saving;
    }

    void OnDisable(){
        Teleport.saveGame -=Saving;
    }

    public void Saving(){
        for(int i=0;i<save.backpack.Count;i++){
            if(save.backpack[i] !=0){

            //items are transfer to backpack, reset items in tempbackpack.
            ItemCount =0;
            }
        }
    }
}
