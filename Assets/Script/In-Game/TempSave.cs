using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TempSave : MonoBehaviour
{
    UserData save;
    double TempHP;
    List<int> TempBackpack;
    int ItemCount =0;
    // Update is called once per frame
    void Update()
    {
        save =GameManager.Instance.currentSaving;
        //when the game start.
        if(save.evaluable ==1){
            TempBackpack =new List<int>(save.backpack.Count -save.itemInBackpack);
            //add items in tempbackpack.

            //count how many items in tempbackpack
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
