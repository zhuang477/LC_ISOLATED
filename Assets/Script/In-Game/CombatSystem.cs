using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public enum AttackPos {Head, Torso, Leg}
    /*
    Head -0
    Torso -1
    Leg -2
    */
    public AttackPos attackPos;
    [HideInInspector]public GameObject Player =null;

    public Control control;
    [HideInInspector]public float rotZ;

    // Update is called once per frame
    void Update()
    {
        if(Player ==null){
            Player =GameObject.Find("Player");
        }else{
            rotZ =control.rotZ;
            if(Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
            {
                if(attackPos >AttackPos.Head){
                    attackPos--;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
            {
                if(attackPos <AttackPos.Leg){
                    attackPos++;
                }
            }
        }
    }
}
