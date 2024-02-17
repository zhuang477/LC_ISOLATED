using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler ,IEndDragHandler//, IPointerClickHandler
{
    public Image image;
    public IconID IconID;
    [HideInInspector] public Transform parentAfterDrag;
    //UserData save =GameManager.Instance.currentSaving;
    //ItemDatabase itemDatabase =GameManager.Instance.itemDatabase;

    public delegate void DisableCollider();
    public static event DisableCollider disableCollider;
    public void OnBeginDrag(PointerEventData eventData){
        parentAfterDrag =transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget =false;
        if(disableCollider !=null){
            disableCollider();
        }
    }

    public void OnDrag(PointerEventData eventData){
        Image IconCheck =gameObject.GetComponent<Image>();
        if(IconCheck.sprite !=null){
            gameObject.transform.position =eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        transform.SetParent(parentAfterDrag);
        image.raycastTarget =true;
    }
    
    /**
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right){
            Debug.Log("xx");
            //in equipping
            if(IconID.SlotID <0){
                for(int i=0;i<save.backpack.Count;i++){
                    if(save.backpack[i] ==0){
                        save.backpack[i] =IconID.ItemID;
                        IconID.SlotID =i;
                        Debug.Log(IconID.SlotID);
                        break;
                    }
                }
            }
            //in backpack
            else{

            }
        }
    }**/
}
