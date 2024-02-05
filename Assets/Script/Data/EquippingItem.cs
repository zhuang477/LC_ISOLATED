using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EquippingItem : MonoBehaviour, IDropHandler
{
    public string EquipmentType;

    public void OnDrop(PointerEventData eventData){
        ItemDatabase itemdatabase=GameManager.Instance.itemDatabase;
        try{
            if(transform.childCount ==0){
                GameObject dropped =eventData.pointerDrag;
                if(itemdatabase.getItem(dropped.GetComponent<IconID>().ItemID).itemType.Equals(EquipmentType)){
                    DragAndDrop draggableItem =dropped.GetComponent<DragAndDrop>();
                    draggableItem.parentAfterDrag =transform;
                }
            }
        }catch(NullReferenceException e){
            throw e;
        }
    }
}
