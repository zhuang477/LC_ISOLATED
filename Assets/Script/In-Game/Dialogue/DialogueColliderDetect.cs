using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueColliderDetect : MonoBehaviour
{
    
    UserData save;

    void Start(){
        save =GameManager.Instance.currentSaving;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<DialogueTrigger>() !=null){
            save.current_Dialogue =other.gameObject.GetComponent<DialogueTrigger>();
        }
    }

    void OnTriggerStay2D(Collider2D other){

    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.GetComponent<DialogueTrigger>() !=null){
            save.current_Dialogue =null;
        }
    }
}
