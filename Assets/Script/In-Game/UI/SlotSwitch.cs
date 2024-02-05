using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotSwitch : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData){
        if(transform.childCount ==0){
            GameObject dropped =eventData.pointerDrag;
            DragAndDrop draggableItem =dropped.GetComponent<DragAndDrop>();
            draggableItem.parentAfterDrag =transform;
        }
    }
}
