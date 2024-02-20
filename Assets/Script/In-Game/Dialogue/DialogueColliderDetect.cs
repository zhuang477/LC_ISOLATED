using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueColliderDetect : MonoBehaviour
{
    public delegate void DialogueRelate();
    public static event DialogueRelate EnableDialogueEvent;
    public static event DialogueRelate DisableDialogueEvent;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<DialogueTrigger>() !=null){
            if(EnableDialogueEvent !=null){
                EnableDialogueEvent();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.GetComponent<DialogueTrigger>() !=null){
            if(EnableDialogueEvent !=null){
                EnableDialogueEvent();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.GetComponent<DialogueTrigger>() !=null){
            if(DisableDialogueEvent !=null){
                DisableDialogueEvent();
            }
        }
    }
}
