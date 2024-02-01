using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other){
        if(Input.GetKey(KeyCode.E)){
            if(other.gameObject.tag.Equals("Player")){
                GameManager.Instance.currentSaving.armor_id =5;
            }
        }
    }
}
