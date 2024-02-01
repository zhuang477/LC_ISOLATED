using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour
{
    public int ItemID;
    public delegate void Interactable();
    public static event Interactable IsPickable;
    void OnEnable(){
        Control.IsPicked +=DestroyThisItem;
    }

    void OnDisable(){
        Control.IsPicked -=DestroyThisItem;
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.tag.Equals("Player")){
            if(IsPickable !=null){
                IsPickable();
            }
        }
    }

    void DestroyThisItem(){
        GameManager.Instance.tempSave.pendingItem.Add(ItemID);
        Destroy(gameObject);
    }
}
