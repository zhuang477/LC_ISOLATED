using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler ,IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData){

    }

    public void OnDrag(PointerEventData eventData){
        Image IconCheck =gameObject.GetComponent<Image>();
        if(IconCheck.sprite.name !="Blank"){
            gameObject.transform.position =eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        
    }
}
